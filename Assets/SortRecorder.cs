using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

public class SortRecorder : MonoBehaviour
{
    [SerializeField] private string filename;
    [SerializeField] private SortManager sm;
    [SerializeField] private ExperimentManager em;
    private Recorder recorder;
    [SerializeField] private TestData td = new TestData();
    private Sorter prevSorter;
    [SerializeField] private int entriesForAvg = 200;
    private int algoIndex;

    private List<float> milliseconds = new List<float>();

    private bool done = false;

    private void Start()
    {
        td = new TestData("hello", new List<int>(), new List<float>(), new List<float>(), new List<float>());
        prevSorter = sm.sorter;
        sm.sorter.OnSorted += OnSorted;
        sm.OnSorterChange += OnSorterChanged;
        em.OnNextStep += OnNextStep;
        em.OnExperimentFinished += OnFinish;

        //DIVIDE BY ENTRIESFORAVG
        foreach (var amount in em.ballAmounts) { td.instances.Add(amount); }
    }

    private void OnSorterChanged(Sorter newSorter)
    {
        prevSorter.OnSorted -= OnSorted;
        newSorter.OnSorted += OnSorted;
        prevSorter = newSorter;
        algoIndex++;
    }

    private void OnSorted()
    {
        if (done) return;
        
        recorder = Recorder.Get("CS_Default");
        if (sm.sorter is CS_DefaultSort)
        {
            recorder = Recorder.Get("CS_Default");
            algoIndex = 0;
        }
        
        if (sm.sorter is Insertionsort)
        {
            recorder = Recorder.Get("Insertion");
            algoIndex = 1;
        }
        
        if (sm.sorter is Mergesort)
        {
            recorder = Recorder.Get("Merge");
            algoIndex = 2;
        }
    }

    private void OnDisable()
    {
        sm.sorter.OnSorted -= OnSorted;
        em.OnNextStep -= OnNextStep;
        sm.OnSorterChange -= OnSorterChanged;
        em.OnExperimentFinished -= OnFinish;
    }

    private void OnNextStep()
    {
        if (recorder == null) return;
        if (recorder.isValid)
        {
            //milliseconds.Add(recorder.elapsedNanoseconds * 0.000001f);
            //if (milliseconds.Count > entriesForAvg)
            //{
            switch (algoIndex)
            {
                case 0:
                    td.ms_CS.Add(recorder.elapsedNanoseconds * 0.000001f);
                    //td.ms_CS.Add(milliseconds.Average());
                    break;
                case 1:
                    td.ms_Insert.Add(recorder.elapsedNanoseconds * 0.000001f);
                    break;
                case 2:
                    td.ms_Merge.Add(recorder.elapsedNanoseconds * 0.000001f);
                    break;
            }
            //milliseconds.Clear();
            //}
        }
    }

    private void OnFinish()
    {
        sm.sorter.OnSorted -= OnSorted;
        WriteResultsToFile();
    }

    private void WriteResultsToFile()
    {
        using (StreamWriter streamWriter = new StreamWriter(filename))
        {
            streamWriter.Write("instances,CS_Default,Insert,Merge");
            streamWriter.WriteLine(String.Empty);
            
            //DO NOT USE td.instancs.count - 1 IT IS TEMPORARY
            
            for (int i = 0; i < td.instances.Count - 1; i++)
            {
                streamWriter.Write($"{td.instances[i].ToString(CultureInfo.InvariantCulture)},{td.ms_CS[i].ToString(CultureInfo.InvariantCulture)},{td.ms_Insert[i].ToString(CultureInfo.InvariantCulture)},{td.ms_Merge[i].ToString(CultureInfo.InvariantCulture)}");
                streamWriter.WriteLine(string.Empty);
            }
        }
    }

    [System.Serializable]
    public struct TestData
    {
        public string tName;
        public List<int> instances;
        public List<float> ms_CS;
        public List<float> ms_Insert;
        public List<float> ms_Merge;

        public TestData(string n, List<int> inst, List<float> _ms_CS, List<float> _ms_Insert, List<float> _ms_Merge)
        {
            tName = n;
            instances = inst;
            ms_CS = _ms_CS;
            ms_Insert = _ms_Insert;
            ms_Merge = _ms_Merge;
        }
    }
}
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
    private Profiler profiler;
    [SerializeField] private SortManager sm;
    [SerializeField] private ExperimentManager em;
    private Recorder recorder;
    [SerializeField] private TestData[] td = new TestData[3];
    [SerializeField] private TestData f_td = new TestData();
    private Sorter prevSorter;
    [SerializeField] private int entriesForAvg = 200;
    private int algoIndex = 0;

    private List<float> milliseconds = new List<float>();

    private bool done = false;

    private void OnEnable()
    {
        prevSorter = sm.sorter;
        sm.sorter.OnSorted += OnSorted;
        sm.OnSorterChange += OnSorterChanged;
        em.OnNextStep += OnChangeStep;
        em.OnExperimentFinished += OnFinish;
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

        //Only sample if the amount is different from last time
        if (recorder.isValid)
        {
            milliseconds.Add(recorder.elapsedNanoseconds * 0.000001f);
            if (milliseconds.Count > entriesForAvg)
            {
                td[algoIndex].instances.Add(sm.balls.Length);
                if (algoIndex == 0)
                {
                    td[algoIndex].ms_CS.Add(milliseconds.Average());
                }
                else if (algoIndex == 1)
                {
                    td[algoIndex].ms_Insert.Add(milliseconds.Average());
                }
                else
                {
                    td[algoIndex].ms_Merge.Add(milliseconds.Average());
                }
                milliseconds.Clear();
            }
        }
    }

    private void OnDisable()
    {
        sm.sorter.OnSorted -= OnSorted;
        em.OnNextStep -= OnChangeStep;
        sm.OnSorterChange -= OnSorterChanged;
        em.OnExperimentFinished -= OnFinish;
    }

    //The data after a step in the experiment (new interval)
    private void OnChangeStep()
    {
        //Avg time for the step, etc
    }

    //The data once the experiment is finished
    private void OnFinish()
    {
        //WriteResultsToFile();
        sm.sorter.OnSorted -= OnSorted;
        WriteResultsToFile(td);
        //WriteResultsToFile();
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //td[i] = new TestData("hello", new List<int>(), new List<float>());
        }
    }

    private void WriteResultsToFile(TestData[] _td)
    {
        using (StreamWriter streamWriter = new StreamWriter(filename))
        {
            streamWriter.Write("instances,CS_Default,Insert,Merge");
            streamWriter.WriteLine(string.Empty);

            //int length = td[0].instances.Count + td[1].instances.Count + td[2].instances.Count;
            //f_td = new TestData("a", new List<int>(length), new List<float>(length));

            
            for (int i = 0; i < 3; i++)
            {
                f_td.instances.AddRange(td[i].instances);
                f_td.ms_CS.AddRange(td[i].ms_CS);
                f_td.ms_Insert.AddRange(td[i].ms_Insert);
                f_td.ms_Merge.AddRange(td[i].ms_Merge);
            }

            
            
            for (int i = 0; i < f_td.instances.Count; i++)
            {
                if (i >= f_td.instances.Count) return;
                streamWriter.Write(f_td.instances[i].ToString(CultureInfo.InvariantCulture) + ",");
                
                if(i < f_td.ms_CS.Count)
                    streamWriter.Write(f_td.ms_CS[i].ToString(CultureInfo.InvariantCulture) + ",");
                
                if(i < f_td.ms_Insert.Count)
                    streamWriter.Write(f_td.ms_Insert[i].ToString(CultureInfo.InvariantCulture) + ",");
                
                if(i < f_td.ms_Merge.Count)
                    streamWriter.Write(f_td.ms_Merge[i].ToString(CultureInfo.InvariantCulture) + ",");
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

        public TestData(string n, List<int> inst, List<float> t1, List<float> t2, List<float> t3)
        {
            tName = n;
            instances = inst;
            ms_CS = t1;
            ms_Insert = t2;
            ms_Merge = t3;
        }
    }
}
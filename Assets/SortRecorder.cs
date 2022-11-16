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
    [SerializeField] private TestData td;
    private Sorter prevSorter;
    [SerializeField] private int entriesForAvg = 200;

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
    }

    private void OnSorted()
    {
        if (done) return;
        if (sm.sorter is CS_DefaultSort) { recorder = Recorder.Get("CS_Default"); }
        if (sm.sorter is Insertionsort)  { recorder = Recorder.Get("Insertion");  }
        if (sm.sorter is Mergesort)      { recorder = Recorder.Get("Merge");      }

        if (recorder.isValid)
        {
            milliseconds.Add(recorder.elapsedNanoseconds * 0.000001f);
            if (milliseconds.Count > entriesForAvg)
            {
                td.name.Add(sm.sorter.sorterName);
                td.ms.Add(milliseconds.Average());
                td.instances.Add(sm.balls.Length);
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

    //The data after a single sort
    //private void OnSorted(float ms)
    //{
    //    //Time taken, etc
    //}

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
        td = new TestData(new List<string>(), new List<int>(), new List<float>());
    }

    //TODO USE 3 TESTDATAS
    //TODO USE 3 TESTDATAS
    //TODO USE 3 TESTDATAS
    //TODO USE 3 TESTDATAS
    //TODO USE 3 TESTDATAS

    private void WriteResultsToFile(TestData _td)
    {
        using (StreamWriter streamWriter = new StreamWriter(filename))
        {
            streamWriter.Write("instances,CS_Default,Insert,Merge");
            streamWriter.WriteLine(string.Empty);

            for (int i = 0; i < _td.name.Count; i++)
            {
                streamWriter.Write(_td.instances[i] + ",");
                streamWriter.Write(1 + ",");
                streamWriter.Write(1 + ",");
                streamWriter.Write(1 + ",");
                //streamWriter.Write(_td.ms[i].ToString(CultureInfo.InvariantCulture) + ",");
                //streamWriter.Write(_td.ms[i + 9].ToString(CultureInfo.InvariantCulture) + ",");
                //streamWriter.Write(_td.ms[i + 19].ToString(CultureInfo.InvariantCulture) + ",");
                streamWriter.WriteLine(string.Empty);
            }
        }
    }

    [System.Serializable]
    public struct TestData
    {
        public List<string> name;
        public List<int> instances;
        public List<float> ms;

        public TestData(List<string> _name, List<int> _instances, List<float> _ms)
        {
            name = _name;
            instances = _instances;
            ms = _ms;
        }
    }
}
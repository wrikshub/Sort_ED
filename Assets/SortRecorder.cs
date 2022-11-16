using System;
using System.Collections;
using System.Collections.Generic;
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

    private List<float> milliseconds = new List<float>();

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
        if (sm.sorter is CS_DefaultSort)
        {
            
        }
        if (sm.sorter is Insertionsort)
        {
            
        }
        if(sm.sorter is Mergesort)
        {
            
        }
        
        if (recorder.isValid)
        {
            print(recorder.elapsedNanoseconds * 0.000001f);
            return;
            milliseconds.Add(recorder.elapsedNanoseconds * 0.000001f);
            if (milliseconds.Count > 200)
            {
                //td.name.Add(sm.sorter.sorterName);
                //td.ms.Add(milliseconds.Average());
                //td.instances.Add(sm.balls.Length);
                //milliseconds.Clear();
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
    }
    
    private void Start()
    {
        td = new TestData(new List<string>(), new List<int>(), new List<float>());
        recorder = Recorder.Get("CS_Default");
        recorder = Recorder.Get("Insertion");
        recorder = Recorder.Get("Merge");
    }

    private void WriteResultsToFile(string algorithm, int instances, float[] ms, float[] fps)
    {
        using (StreamWriter streamWriter = new StreamWriter(filename))
        {
            streamWriter.Write($"instances: {instances}");


            //if (0 < columnIdentifiers.Count)
            //{
            //    streamWriter.Write("; ");
            //}
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
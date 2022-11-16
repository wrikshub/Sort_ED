using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SortRecorder : MonoBehaviour
{
    [SerializeField] private string filename;
    [SerializeField] private string identifier = "{0} FPS";
    private UnityEngine.Profiling.Profiler profiler;
    [SerializeField] private SortManager sm;
    [SerializeField] private ExperimentManager em;

    private void OnEnable()
    {
        sm.sorter.OnSorted += OnSorted;
        em.OnNextStep += OnChangeStep;
        em.OnExperimentFinished += OnFinish;
    }

    private void OnDisable()
    {
        sm.sorter.OnSorted -= OnSorted;
        em.OnNextStep -= OnChangeStep;
        em.OnExperimentFinished -= OnFinish;
    }
    
    //The data after a single sort
    private void OnSorted(float ms)
    {
        //Time taken, etc
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
}
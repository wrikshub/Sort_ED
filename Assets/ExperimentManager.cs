using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    [SerializeField] private SortManager _sortManager;
    [SerializeField] private float experimentDuration = 60f;
    [SerializeField] private float interval = 2f;
    [SerializeField] private int amount = 100;
    [SerializeField] private Sorter[] sorters;
    private bool running = true;
    private int sorterIndex = 0;
    public float timeTaken = 0f;
    public float timeTakenSmall = 0f;
    private TestData td;

    private void Start()
    {
        running = true;
        sorterIndex = 0;
        StartExperiment();
    }

    private void Update()
    {
        if (!running) return;
        
        float dt = Time.deltaTime;
        timeTakenSmall += dt;
        timeTaken += dt;

        if (timeTakenSmall > interval)
        {
            timeTakenSmall = 0;
            _sortManager.AddBalls(amount);

            SampleSorts();
        }

        if (timeTaken >= experimentDuration)
        {
            NextExperiment();
            timeTaken = 0;
            _sortManager.Clear();
            //RecordData
        }
    }

    private void SampleSorts()
    {
        //Sample sorting-data here
        //td.ms
        //td.fps
        //td.name
    }
    
    private void StartExperiment()
    {
        _sortManager.AddBalls(amount);
    }

    private void RestartExperiment()
    {
        _sortManager.Clear();
    }

    private void NextExperiment()
    {
        sorterIndex++;
        if (sorterIndex >= sorters.Length)
        {
            EndExperiment();
        }
        else
        {
            _sortManager.sorter = sorters[sorterIndex];
        }
    }

    private void EndExperiment()
    {
        running = false;
        _sortManager.Clear();
        //Final output data collected
    }
}

struct TestData
{
    public string[] name;
    public float[] ms;
    public float[] fps;
}
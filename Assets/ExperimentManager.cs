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
    public int cumulativeAmount = 0;
    [SerializeField] private Sorter[] sorters;
    private bool running = true;
    private int sorterIndex = 0;
    public float timeTaken = 0f;
    public float timeTakenSmall = 0f;

    public event OntoNextStep OnNextStep;
    public delegate void OntoNextStep();

    public event ExperimentFinished OnExperimentFinished;
    public delegate void ExperimentFinished();

    private void Start()
    {
        running = true;
        sorterIndex = 0;
        cumulativeAmount = amount;
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
            NextStep();
            SampleSorts();
        }

        if (timeTaken >= experimentDuration)
        {
            timeTaken = 0;
            _sortManager.Clear();
            NextExperiment();
        }
    }

    private void NextStep()
    {
        timeTakenSmall = 0;
        cumulativeAmount += amount;
        _sortManager.Clear();
        _sortManager.AddBalls(cumulativeAmount);
        OnNextStep?.Invoke();
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
    
    private void NextExperiment()
    {
        cumulativeAmount = 0;
        sorterIndex++;
        if (sorterIndex >= sorters.Length)
        {
            EndExperiment();
        }
        else
        {
            _sortManager.ChangeSorter(sorters[sorterIndex]);
            //_sortManager.sorter = sorters[sorterIndex];
        }
    }

    private void EndExperiment()
    {
        running = false;
        _sortManager.Clear();
        //Final output data collected
        
        OnExperimentFinished?.Invoke();
    }
}
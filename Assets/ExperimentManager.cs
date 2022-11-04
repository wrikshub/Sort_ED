using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    [SerializeField] private SortManager _sortManager;
    [SerializeField] private float experimentDuration = 60f;
    [SerializeField] private int interval = 100;
    [SerializeField] private Sorter[] sorters;
    private bool running = true;
    private int sorterIndex = 0;
    private float timeTaken = 0f;
    private void Start()
    {
        running = true;
        sorterIndex = 0;
        StartExperiment();
    }

    private void Update()
    {
        if (!running) return;
        
        ExperimentEveryFrame();
        
        timeTaken += Time.deltaTime;
        if (timeTaken >= experimentDuration)
        {
            NextExperiment();
            experimentDuration = 0;
        }
    }

    private void StartExperiment()
    {
        _sortManager.AddBalls(100);
    }

    private void RestartExperiment()
    {
        _sortManager.Clear();
    }
    
    private void ExperimentEveryFrame()
    {
        //Add() balls in intervals here
        
    }
    
    private void NextExperiment()
    {
        sorterIndex++;
        if (sorterIndex > sorters.Length)
        {
            EndExperiment();
        }

        _sortManager.sorter = sorters[sorterIndex];
    }

    private void EndExperiment()
    {
        running = false;
        
        //Output data collected
    }
}

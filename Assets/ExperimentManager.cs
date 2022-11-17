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
    private int cumulativeAmount = 0;
    [SerializeField] private Sorter[] sorters;
    private bool running = true;
    private int sorterIndex = 0;
    public float timeTaken = 0f;
    public float timeTakenSmall = 0f;
    [SerializeField] private float timeScale = 1f;

    public event OntoNextStep OnNextStep;
    public delegate void OntoNextStep();
    public event ExperimentFinished OnExperimentFinished;
    public delegate void ExperimentFinished();
    
    private void Start()
    {
        Time.timeScale = timeScale;
        running = true;
        sorterIndex = 0;
        cumulativeAmount = amount;
        StartExperiment();
    }

    //Run in ienumerator?
    private void Update()
    {
        if (!running) return;

        timeTakenSmall += Time.deltaTime;
        if (timeTakenSmall > interval)
        {
            timeTakenSmall = 0;
            NextStep();
        }

        timeTaken += Time.deltaTime;
        if (timeTaken > experimentDuration)
        {
            timeTaken = 0;
            _sortManager.Clear();
            NextExperiment();
        }
    }
    
    private void NextStep()
    {
        cumulativeAmount += amount;
        _sortManager.Clear();
        _sortManager.AddBalls(cumulativeAmount);
        OnNextStep?.Invoke();
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
        }
    }

    private void EndExperiment()
    {
        running = false;
        _sortManager.Clear();
        OnExperimentFinished?.Invoke();
    }
}
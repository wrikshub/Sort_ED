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
    public float waitTime = 0f;
    [SerializeField] private float timeScale = 1f;
    public List<int> ballAmounts = new List<int>();
    private int amountOfTimes = 0;

    public event OntoNextStep OnNextStep;
    public delegate void OntoNextStep();
    public event ExperimentFinished OnExperimentFinished;
    public delegate void ExperimentFinished();

    private void Awake()
    {
        Time.timeScale = timeScale;

        float max = amount * (experimentDuration / interval);
        amountOfTimes = (int) (experimentDuration / interval);
        
        for (int i = 1; i < amountOfTimes + 2; i++)
        {
            ballAmounts.Add(amount * i);
        }
    }

    private void Start()
    {
        StartCoroutine(RunExperiment());
    }

    IEnumerator RunExperiment()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i > 0)
                _sortManager.ChangeSorter(sorters[i]);
            
            for (int j = 0; j < amountOfTimes + 1; j++)
            {
                NextStep(j);
                yield return new WaitForSeconds(waitTime);
            }
        }

        EndExperiment();
    }

    private void NextStep(int index)
    {
        _sortManager.Clear();
        _sortManager.AddBalls(ballAmounts[index]);
        OnNextStep?.Invoke();
    }

    private void StartExperiment()
    {
        _sortManager.AddBalls(amount);
    }

    private void EndExperiment()
    {
        _sortManager.Clear();
        OnExperimentFinished?.Invoke();
    }
}
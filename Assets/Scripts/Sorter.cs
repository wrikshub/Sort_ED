using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class Sorter : ScriptableObject
{
    [SerializeField] internal string sorterName;
    internal CustomSampler sampler;

    public abstract void Begin();
    public abstract Ball[] Sort(Ball[] ball);
    public SortOver OnSorted;
    public delegate void SortOver();
}
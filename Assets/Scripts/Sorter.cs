using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sorter : ScriptableObject
{
    [SerializeField] internal string sorterName;
    public abstract Ball[] Sort(Ball[] ball);
}

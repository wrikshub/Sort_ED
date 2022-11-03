using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sorter : ScriptableObject
{
    public abstract void Sort(ref List<Ball> balls);
}

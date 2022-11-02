using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sorter : ScriptableObject
{
    public virtual void Sort(ref List<Ball> balls) { }
}

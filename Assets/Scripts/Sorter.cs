using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sorter : ScriptableObject
{
    public abstract Ball[] Sort(Ball[] ball);
}

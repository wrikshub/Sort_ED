using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Sort/CS Default Sort")]
public class CS_DefaultSort : Sorter
{
    public override Ball[] Sort(Ball[] balls)
    {
        Profiler.BeginSample("Trial");
        balls = balls.OrderBy(x => x.DstFromTarget).ToArray();
        Profiler.EndSample();
        return balls;
    }
}

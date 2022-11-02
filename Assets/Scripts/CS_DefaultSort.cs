using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Sort/CS Default Sort")]
public class CS_DefaultSort : Sorter
{
    public override void Sort(ref List<Ball> balls)
    {
        base.Sort(ref balls);
        Profiler.BeginSample("Trial");
        balls = balls.OrderBy(balls => balls.DstFromTarget).ToList();
        Profiler.EndSample();
    }
}

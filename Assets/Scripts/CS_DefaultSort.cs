using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Sort/CS Default Sort")]
public class CS_DefaultSort : Sorter
{
    public override void Begin()
    {
        sampler = CustomSampler.Create("CS_Default");
    }

    public override Ball[] Sort(Ball[] balls)
    {
        sampler.Begin();
        balls = balls.OrderBy(x => x.DstFromTarget).ToArray();
        
        OnSorted?.Invoke();
        sampler.End();
            
        return balls;
    }
}

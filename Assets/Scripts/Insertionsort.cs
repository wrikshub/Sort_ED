using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Sort/Insertionsort")]
public class Insertionsort : Sorter
{
    public override void Begin()
    {
        sampler = CustomSampler.Create("Insertion");
    }

    public override Ball[] Sort(Ball[] balls)
    {
        sampler.Begin();
        Insertion(balls);
        
        OnSorted?.Invoke();
        sampler.End();
        
        return balls;
    }

    private static void Insertion(Ball[] balls)
    {
        for (int i = 1; i < balls.Length; i++)
        {
            Ball ball = balls[i];
            int j = i;
            while (j > 0 && balls[j - 1].DstFromTarget > ball.DstFromTarget)
            {
                balls[j] = balls[j - 1];
                j--;
            }

            balls[j] = ball;
        }
    }
}

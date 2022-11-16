using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Sort/Insertionsort")]
public class Insertionsort : Sorter
{
    public override Ball[] Sort(Ball[] balls)
    {
        for (int i = 1; i < balls.Length; i++) {
            Ball ball = balls[i];
            int j = i;
            while (j > 0 && balls[j - 1].DstFromTarget > ball.DstFromTarget) {
                balls[j] = balls[j - 1];
                j--;
            }
            balls[j] = ball;
        }
        
        OnSorted?.Invoke(1.25f);
        
        return balls;
    }
}

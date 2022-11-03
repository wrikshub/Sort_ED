using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Sort/Insertionsort")]
public class Insertionsort : Sorter
{
    public override void Sort(ref List<Ball> balls)
    {
        var list = balls;
        for (int i = 1; i < list.Count; i++)
        {
            for (int j = i; 0 < j && list[i].DstFromTarget < list[j - 1].DstFromTarget; j--)
            {
                float temp = list[j - 1].DstFromTarget;
                list[j - 1] = list[j];
                list[j].DstFromTarget = temp;
            }
        }
        balls = list;

        for (int i = 0; i < balls.Count; i++)
        {
            Debug.Log( i + " " + balls[i].DstFromTarget);
        }
    }
}

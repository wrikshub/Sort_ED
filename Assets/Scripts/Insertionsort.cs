using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Sort/Insertionsort")]
public class Insertionsort : Sorter
{
    public override void Sort(Ball[] balls)
    {
        for (int i = 0; i < balls.Length; i++)
        {
            for (int j = i; 0 < j && balls[i].DstFromTarget < balls[j - 1].DstFromTarget; j--)
            {
                float temp = balls[j - 1].DstFromTarget;
                balls[j - 1].DstFromTarget = balls[j].DstFromTarget;
                balls[j].DstFromTarget = temp;
            }
        }

        for (int i = 0; i < balls.Length; i++)
        {
            Debug.Log( i + " " + balls[i].DstFromTarget);
        }
    }
}

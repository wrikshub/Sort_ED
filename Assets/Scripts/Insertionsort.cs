using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "Sort/Insertionsort")]
public class Insertionsort : Sorter
{
    //https://en.wikipedia.org/wiki/Insertion_sort
    public override Ball[] Sort(Ball[] balls)
    {
        int i = 1;
        while (i < balls.Length) {
            int j = i;
            while (j > 0 && balls[j - 1].DstFromTarget > balls[j].DstFromTarget) {
                (balls[j], balls[j - 1]) = (balls[j - 1], balls[j]);
            }
            i = i + 1;
        }
        
        return balls;
    }
}

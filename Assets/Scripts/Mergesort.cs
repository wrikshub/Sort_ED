using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sort/Mergesort")]
public class Mergesort : Sorter
{
    //https://en.wikipedia.org/wiki/Merge_sort
    public override Ball[] Sort(Ball[] balls)
    {
        var temp = balls;
        MergeSort(balls, temp, 0);
        //fix :p
        return temp;
    }

    private void MergeSort(Ball[] A, Ball[] B, int n)
    {
        B = A;
        SplitMerge(B, 0, n, A);
    }
    
    private void SplitMerge(Ball[] B, int begin, int end, Ball[] A)
    {
        if (end - begin <= 1)
            return;

        int middle = (end + begin) / 2;
        
        SplitMerge(A, begin, middle, B);
        SplitMerge(A, middle, end, B);
        Merge(B, begin, middle, end, A);
    }

    private void Merge(Ball[] A, int begin, int middle, int end, Ball[] B)
    {
        int i = begin;
        int j = middle;

        for (int k = begin; k < end; k++)
        {
            if (i < middle && (j >= end || A[i].DstFromTarget <= A[j].DstFromTarget))
            {
                B[k] = A[i];
                i++;
            }
            else
            {
                B[k] = A[j];
                j++;
            }
        }
    }
}

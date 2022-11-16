using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

[CreateAssetMenu(menuName = "Sort/Mergesort")]
public class Mergesort : Sorter
{
    //https://www.geeksforgeeks.org/merge-sort/
    public override void Begin()
    {
        sampler = CustomSampler.Create("Merge");
    }

    public override Ball[] Sort(Ball[] _balls)
    {
        sampler.Begin();
        MergeSort(_balls, 0, _balls.Length - 1);
        
        OnSorted?.Invoke();
        sampler.End();
        return _balls;
    }

    void Merge(Ball[] balls, int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        Ball[] L = new Ball[n1];
        Ball[] M = new Ball[n2];
        int i, j;
        
        for (i = 0; i < n1; ++i)
            L[i] = balls[l + i];
        for (j = 0; j < n2; ++j)
            M[j] = balls[m + 1 + j];

        i = 0;
        j = 0;
        int k = l;

        while (i < n1 && j < n2)
        {
            if (L[i].DstFromTarget <= M[j].DstFromTarget)
            {
                balls[k] = L[i];
                i++;
            }
            else
            {
                balls[k] = M[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            balls[k] = L[i];
            i++;
            k++;
        }
        
        while (j < n2)
        {
            balls[k] = M[j];
            j++;
            k++;
        }
    }

    void MergeSort(Ball[] balls, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            
            MergeSort(balls, l, m);
            MergeSort(balls, m + 1, r);
            Merge(balls, l, m, r);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using Random = UnityEngine.Random;

public class SortManager : MonoBehaviour
{
    [Header("Sorting algorithm")] public Sorter sorter;

    [Header("Ball")] public GameObject ball;
    public float minSpeed = 0f;
    public float maxSpeed = 2f;

    [Header("Target")] public Transform target;

    [Header("Bounds")] public Vector2 bounds;

    [Header("Amount of balls")] public int minAmount;
    public int maxAmount;

    //TODO ONVALIDATE !!! CANNOT BE HIGHER THAN MINAMOUNT
    [Header("Threshold")] public int threshold = 1;

     public List<Ball> balls = new List<Ball>();

    public void GenerateBalls()
    {
        int length = Random.Range(minAmount, maxAmount);
        for (int i = 0; i < length; i++)
        {
            var theball = Instantiate(ball, Vector3.zero, Quaternion.identity);
            theball.transform.parent = transform;

            theball.GetComponent<Ball>().InitBall(target,
                Random.Range(minSpeed, maxSpeed),
                new Vector2(Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)).normalized,
                bounds, this);

            balls.Add(theball.GetComponent<Ball>());
        }
    }

    private void Start()
    {
        StartSimulation();
    }

    private void ResetBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].DstFromTarget = balls[i].GetDistance();
        }
    }
    
    void Update()
    {
        
        Sort();

        //TODO Move this to Ball.cs
        if (balls.Count == 0) return;
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].IsWithin = false;
        }

        if (balls.Count < threshold) return;
        for (int i = 0; i < threshold; i++)
        {
            balls[i].IsWithin = true;
        }
    }

    public void Sort()
    {
        sorter.Sort(ref balls);
    }

    public void StartSimulation()
    {
        GenerateBalls();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteAlways]
public class SortManager : MonoBehaviour
{
    [Header("Sorting algorithm")]
    public Sorter sorter;

    [Header("Ball")]
    public GameObject ball;
    public float minSpeed = 0f;
    public float maxSpeed = 2f;

    [Header("Target")]
    public Transform target;

    [Header("Bounds")]
    public Vector2 bounds;

    [Header("Amount of balls")] public int minAmount;
    public int maxAmount;

    //[Header("Numbers")]
    public List<Transform> balls = new List<Transform>();
    public bool Running { private set; get; }

    public void GenerateBalls()
    {
        StopSimulation();

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
            
            balls.Add(theball.transform);
        }

        Running = true;
    }

    public void Sort()
    {
        
    }

    public void StartSimulation()
    {
        GenerateBalls();
    }

    public void PauseSimulation()
    {
        Running = !Running;
    }

    public void StopSimulation()
    {
        for (int i = 0; i < balls.Count; i++) DestroyImmediate(balls[i].gameObject);
        balls.Clear();
    }
}
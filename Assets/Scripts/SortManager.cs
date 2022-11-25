using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Header("Threshold")] public bool thresholdIsHalf = false;
    public int threshold = 1;

    public Ball[] balls;
    
    public event SorterChange OnSorterChange;
    public delegate void SorterChange(Sorter sorter);
    
    public void GenerateBalls()
    {
        int length = Random.Range(minAmount, maxAmount);
        balls = new Ball[length];
        for (int i = 0; i < length; i++)
        {
            var theball = Instantiate(ball, Vector3.zero, Quaternion.identity);
            theball.transform.parent = transform;

            var ballComponent = theball.GetComponent<Ball>();
            
            ballComponent.InitBall(target,
                Random.Range(minSpeed, maxSpeed),
                new Vector2(Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)).normalized,
                bounds, this);

            balls[i] = ballComponent;
        }
    }

    private void Start()
    {
        StartSimulation();
    }

    public void ChangeSorter(Sorter _sorter)
    {
        sorter = _sorter;
        sorter.Begin();
        OnSorterChange?.Invoke(_sorter);
    }
    
    void Update()
    {
        Sort();

        if(thresholdIsHalf)
            threshold = balls.Length / 2;
        
        
        ChangeColors();

        /*
        foreach (var ball in balls)
        {
            ball.SetColor(Color.white);
        }
        */
        
    }
    
    private void Sort()
    {
        balls = sorter.Sort(balls);
    }

    public void Clear()
    {
        for (int i = balls.Length - 1; i >= 0; i--)
        {
            Destroy(balls[i].gameObject);
        }
        balls = Array.Empty<Ball>();
    }
    
    public void AddBalls(int amount)
    {
        Ball[] tempBalls = balls;
        balls = new Ball[balls.Length + amount];

        //DRY !!! same as generateballs
        for (int i = tempBalls.Length; i < balls.Length; i++)
        {
            var theball = Instantiate(ball, new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);
            theball.transform.parent = transform;

            var ballComponent = theball.GetComponent<Ball>();
            
            ballComponent.InitBall(target,
                Random.Range(minSpeed, maxSpeed),
                new Vector2(Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)).normalized,
                bounds, this);

            balls[i] = ballComponent;
        }
        
        for (int i = 0; i < tempBalls.Length; i++)
        {
            balls[i] = tempBalls[i];
        }
    }
    
    public void StartSimulation()
    {
        GenerateBalls();
        sorter.Begin();
    }

    public void ChangeColors()
    {
        if (balls.Length == 0) return;
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].IsWithin = false;
        }

        if (balls.Length < threshold) return;
        for (int i = 0; i < threshold; i++)
        {
            balls[i].IsWithin = true;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class BallStyle : Ball
{
    private LineRenderer lr;
    private Transform target;
    
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        target = GameObject.FindWithTag("Target").transform;
    }

    internal override void Update()
    {
        base.Update();
        
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.position);
    }
}

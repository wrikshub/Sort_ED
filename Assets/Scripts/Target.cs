using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private SortManager sm;
    [SerializeField] private Transform circle;
    private void Update()
    {
        if(sm.threshold > 0 && sm.balls.Length > 0)
            circle.localScale = Vector3.one * (transform.position.magnitude - sm.balls[sm.threshold - 1].transform.position.magnitude) * 2f;
    }
}

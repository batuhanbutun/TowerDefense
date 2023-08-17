using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;
    public static Transform[] points2;
    public static Transform[] points3;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for(int i = 0; i < points.Length;i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}

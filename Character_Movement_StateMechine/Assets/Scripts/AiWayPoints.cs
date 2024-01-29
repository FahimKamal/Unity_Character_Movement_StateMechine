using System;
using System.Collections.Generic;
using UnityEngine;

public class AiWayPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;

    public List<Transform> WayPointList
    {
        get => wayPoints;
        private set => wayPoints = value;
    }

    private void Awake()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        WayPointList.Clear();
        foreach (Transform t in transform)
        {
            WayPointList.Add(t);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var wayPoint in WayPointList)
        {
            Gizmos.DrawWireSphere(wayPoint.position, 0.5f);
        }
    }
    
    
} // class


using System;
using System.Collections.Generic;
using UnityEngine;

public class AiWayPoints : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;

    public List<Transform> WayPointList => wayPoints;

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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var wayPoint in WayPointList)
        {
            Gizmos.DrawWireSphere(wayPoint.position, 0.5f);
        }
    }
#endif
    
} // class


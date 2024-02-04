using System;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private Vector3 standPointUp;
    [SerializeField] private Vector3 standPointDown;
    [SerializeField] private Vector3 standPointLeft;
    [SerializeField] private Vector3 standPointRight;

    public List<Vector3> StandPoints { get; private set; }

    private void OnValidate()
    {
        
    }

    private void Start()
    {
        StandPoints = new List<Vector3>(){standPointUp, standPointDown, standPointLeft, standPointRight};
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + standPointUp, 0.3f);
        Gizmos.DrawWireSphere(transform.position + standPointDown, 0.3f);
        Gizmos.DrawWireSphere(transform.position + standPointLeft, 0.3f);
        Gizmos.DrawWireSphere(transform.position + standPointRight, 0.3f);
    }
}

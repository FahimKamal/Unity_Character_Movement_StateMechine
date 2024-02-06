using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Serializable]
    public class PositionRotation
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;

        public PositionRotation(string name, Vector3 position, Quaternion rotation)
        {
            this.name = name;
            this.position = position;
            this.rotation = rotation;
        }
    }
    
    [Header("Tree Property")]
    [Tooltip("The time it takes to cut the tree")]
    [SerializeField] private float cuttingTime = 10.0f;
    [SerializeField] private float health = 100.0f;

    [Header("NPC Stand Positions")]
    [SerializeField] private bool showLocation = true;
    [SerializeField] private Mesh arrowMesh;

    [SerializeField] private List<PositionRotation> standPoints;
    private List<PositionRotation> _standPointsTransform;

    public float CuttingTime => cuttingTime;

    public float Health => health;


    private void Awake()
    {
        _standPointsTransform = new List<PositionRotation>();
        ValidatePositions();
    }

    /// <summary>
    /// Upon giving a vector3 location, method returns the closest standpoint of the tree that are created
    /// but developer around the tree. 
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    public PositionRotation ClosestStandPosition(Vector3 center)
    {
        PositionRotation closest = null;
        float closestDistance = float.MaxValue;
        foreach (var positionRotation in _standPointsTransform)
        {
            float distance = Vector3.Distance(positionRotation.position, center);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = positionRotation;
            }
        }

        return closest;
    }
    
    /// <summary>
    /// Internal method to calculate the location and rotation of standpoints.
    /// </summary>
    private void ValidatePositions()
    {
        var rootTran = transform;
        
        _standPointsTransform?.Clear();
        if (standPoints != null)
        {
            foreach (var variable in standPoints)
            {
                var standTrans = new PositionRotation(
                    variable.name,
                    rootTran.position + variable.position,
                    rootTran.rotation * variable.rotation);
                _standPointsTransform.Add(standTrans);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showLocation) return;
        
        Gizmos.color = Color.blue;
        var objTransform = transform;
        foreach (var standPoint in standPoints)
        {
            
            Gizmos.DrawWireMesh(arrowMesh, 
                objTransform.position + standPoint.position, 
                objTransform.rotation * standPoint.rotation, 
                new Vector3(1f, 1f, 1f));
        }
    }
    
} // class

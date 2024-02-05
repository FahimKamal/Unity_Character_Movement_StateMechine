using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
    public List<PositionRotation> _standPointsTransform;

    public List<PositionRotation> StandPointsTransform => _standPointsTransform;

    public float CuttingTime => cuttingTime;

    public float Health => health;


    private void Awake()
    {
        ValidatePositions();
    }

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
    

    [Button]
    private void ValidatePositions()
    {
        var rootTran = transform;
        
        _standPointsTransform?.Clear();
        if (standPoints != null)
        {
            foreach (var VARIABLE in standPoints)
            {
                var standTrans = new PositionRotation(
                    VARIABLE.name,
                    rootTran.position + VARIABLE.position,
                    rootTran.rotation * VARIABLE.rotation);
                _standPointsTransform.Add(standTrans);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showLocation)
        {
            Gizmos.color = Color.blue;
            foreach (var positionRotation in _standPointsTransform)
            {
                Gizmos.DrawWireMesh(arrowMesh, positionRotation.position, positionRotation.rotation, new Vector3(1f, 1f, 1f));
            }
            
        }
    }
    
} // class

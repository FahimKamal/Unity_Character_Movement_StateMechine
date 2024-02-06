using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionRotation
{
    public string name;
    public Vector3 position;
    public Quaternion rotation = Quaternion.identity;

    public PositionRotation(string name, Vector3 position, Quaternion rotation)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
    }
}

[RequireComponent(typeof(Collider))]
public abstract class NpcInteractable: MonoBehaviour
{
    [Header("NPC Stand Positions")]
    [SerializeField] private bool showLocation = true;
    [SerializeField] private Mesh arrowMesh;

    [SerializeField] private List<PositionRotation> standPoints;
    private List<PositionRotation> _standPointsTransform;
    private Collider  _collider;

    public Collider AttachedCollider => _collider;

    private void Awake()
    {
        _standPointsTransform = new List<PositionRotation>();
        _collider = GetComponent<Collider>();
        ValidatePositions();
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
                    variable.rotation);
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
                standPoint.rotation, 
                new Vector3(1f, 1f, 1f));
        }
    }
    
    /// <summary>
    /// Upon giving a vector3 location, method returns the closest standpoint of the tree that are created
    /// but developer around the tree. 
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    public PositionRotation ClosestStandPositionAndRotation(Vector3 center)
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
    
    
} // class 
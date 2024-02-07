using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class WayPoints
{
    public bool showWayPoints = true;
    public Mesh arrowMesh;
    public List<Vector3> wayPoints = new List<Vector3>();
    public List<Quaternion> wayPointsRotation = new List<Quaternion>();

    [HideInInspector] public Transform parentTransform;
    
    public void ValidateWayPoint(Transform transform)
    {
        parentTransform = transform;
        // if no waypoints, set current position as waypoint.
        if (wayPoints.Count <= 0)
        {
            wayPoints.Add(Vector3.zero);
            wayPointsRotation.Clear();
            wayPointsRotation.Add(Quaternion.identity);
            
            return;
        }
        
        ValidateRotation();
    }

    public void ValidateRotation()
    {
        if (wayPointsRotation != null)
        {
            List<Quaternion> listCopy = new List<Quaternion>(wayPointsRotation);

            var wayPointsCount = wayPoints.Count;
            wayPointsRotation.Clear();

            for (int i = 0; i < wayPointsCount; i++)
            {
                if (i <= listCopy.Count - 1)
                {
                    var temp = listCopy[i];
                    wayPointsRotation.Add(temp);
                    continue;
                }
                wayPointsRotation.Add(Quaternion.identity);
            }
        }
    }

    public void Draw()
    {
        if (!showWayPoints)
        {
            return;
        }

        var parentPosition = parentTransform.position;
        for (int i = 0; i < wayPoints.Count; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireMesh(arrowMesh, parentPosition + wayPoints[i], wayPointsRotation[i], Vector3.one);
            UnityEditor.Handles.Label(parentPosition + wayPoints[i], "Waypoint " + (i+1));
        }
    }
}

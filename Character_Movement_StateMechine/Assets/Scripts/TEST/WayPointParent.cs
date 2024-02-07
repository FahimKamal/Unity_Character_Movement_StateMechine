using System;
using UnityEngine;

public class WayPointParent : MonoBehaviour
{
    public WayPoints wayPoints;

    private void OnValidate()
    {
        if (wayPoints != null)
        {
            wayPoints.ValidateWayPoint(transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        wayPoints.Draw();
    }
}

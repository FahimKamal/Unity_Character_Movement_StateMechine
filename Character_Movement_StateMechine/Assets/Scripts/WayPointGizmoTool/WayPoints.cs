using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WayPointGizmoTool
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField] internal bool ShowWayPoints = true;
        [SerializeField] private bool showConnection = false;
        [SerializeField] private Mesh arrowMesh;
        [SerializeField] internal List<Vector3> positions = new List<Vector3>();
        [SerializeField] internal List<Quaternion> rotations = new List<Quaternion>();

        internal Transform parentTransform;

        public (Vector3 position, Quaternion rotation) GetPositionRotationAtIndex(int index)
        {
            return (positions[index], rotations[index]);
        }
        
        public (Vector3 position, Quaternion rotation) GetClosestPositionRotation(Vector3 center)
        {
            var closestIndex = 0;
            var closestDistance = float.MaxValue;
            for (int i = 0; i < positions.Count; i++)
            {
                var distance = Vector3.Distance(center, positions[i]);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            return (positions[closestIndex], rotations[closestIndex]);
        }
        
        private void OnValidate()
        {
            if (positions != null)
            {
                ValidateWayPoint(transform);
            }
        }
        
        private void ValidateWayPoint(Transform transform)
        {
            parentTransform = transform;
            // if no waypoints, set current position as waypoint.
            if (positions.Count <= 0)
            {
                positions.Add(Vector3.zero);
                rotations.Clear();
                rotations.Add(Quaternion.identity);
            
                return;
            }
        
            ValidateRotation();
        }

        private void ValidateRotation()
        {
            if (rotations != null)
            {
                List<Quaternion> listCopy = new List<Quaternion>(rotations);

                var wayPointsCount = positions.Count;
                rotations.Clear();

                for (int i = 0; i < wayPointsCount; i++)
                {
                    if (i <= listCopy.Count - 1)
                    {
                        var temp = listCopy[i];
                        rotations.Add(temp);
                        continue;
                    }
                    rotations.Add(Quaternion.identity);
                }
            }
        }

        private void Draw()
        {
            if (!ShowWayPoints)
            {
                return;
            }

            var parentPosition = parentTransform.position;
            for (int i = 0; i < positions.Count; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireMesh(arrowMesh, parentPosition + positions[i], rotations[i], Vector3.one);
                Gizmos.DrawSphere(parentPosition + positions[i], 0.1f);
                UnityEditor.Handles.Label(parentPosition + positions[i], "Waypoint " + (i+1));

                if (showConnection && positions.Count > 1)
                {
                    Gizmos.color = Color.grey;
                    if (i == 0)
                    {
                        Gizmos.DrawLine(parentPosition + positions[0], parentPosition + positions[1]);
                    }
                    else if (i == positions.Count - 1)
                    {
                        Gizmos.DrawLine(parentPosition +positions[i - 1], parentPosition + positions[i]);
                        // Gizmos.color = Color.grey;
                        Gizmos.DrawLine(parentPosition + positions[positions.Count - 1], parentPosition + positions[0]);
                    }
                    else
                    {
                        // Gizmos.color = Color.blue;
                        Gizmos.DrawLine(parentPosition + positions[i - 1], parentPosition + positions[i]);
                    }
                }
                
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Draw();
        }
    }
    
#if UNITY_EDITOR

    [CustomEditor(typeof(WayPoints))]
    public class WayPointsEditor : UnityEditor.Editor
    {
        protected virtual void OnSceneGUI()
        {
            WayPoints wayPoints = (WayPoints)target;

            if (wayPoints.positions == null) return;

            if (!wayPoints.ShowWayPoints)
            {
                return;
            }

            for (int i = 0; i < wayPoints.positions.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(wayPoints.parentTransform.position + wayPoints.positions[i], wayPoints.rotations[i]);
                var newRotation = Handles.RotationHandle(wayPoints.rotations[i],
                    wayPoints.parentTransform.position + wayPoints.positions[i]);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(wayPoints, "Move Waypoint");
                    wayPoints.positions[i] = newPosition - wayPoints.transform.position;
                    wayPoints.rotations[i] = newRotation;
                }
            }
        }
    }
    
#endif
    
}



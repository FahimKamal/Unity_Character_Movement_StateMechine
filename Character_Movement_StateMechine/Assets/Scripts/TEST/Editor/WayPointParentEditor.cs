using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPointParent))]
public class WayPointParentEditor : Editor
{
    private SerializedProperty wayPointsProperty;

    void OnEnable()
    {
        wayPointsProperty = serializedObject.FindProperty("wayPoints");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(wayPointsProperty);
        serializedObject.ApplyModifiedProperties();
    }

    protected virtual void OnSceneGUI()
    {
        WayPointParent wayPointParent = (WayPointParent)target;

        if (wayPointParent.wayPoints == null) return;

        if (!wayPointParent.wayPoints.showWayPoints)
        {
            return;
        }

        for (int i = 0; i < wayPointParent.wayPoints.wayPoints.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPosition = Handles.PositionHandle(wayPointParent.wayPoints.parentTransform.position + wayPointParent.wayPoints.wayPoints[i], wayPointParent.wayPoints.wayPointsRotation[i]);
            var newRotation = Handles.RotationHandle(wayPointParent.wayPoints.wayPointsRotation[i],
                wayPointParent.wayPoints.parentTransform.position + wayPointParent.wayPoints.wayPoints[i]);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPointParent, "Move Waypoint");
                wayPointParent.wayPoints.wayPoints[i] = newPosition - wayPointParent.transform.position;
                wayPointParent.wayPoints.wayPointsRotation[i] = newRotation;
            }
        }
    }
}
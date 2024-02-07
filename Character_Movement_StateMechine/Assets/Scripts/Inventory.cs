using UnityEngine;
using UnityEngine.Serialization;
using WayPointGizmoTool;

namespace Scripts
{
    [RequireComponent(typeof(WayPoints))]
    public class Inventory : NpcInteractable
    {
        [SerializeField] private WayPoints wayPoints;
        private void OnValidate()
        {
            wayPoints ??= GetComponent<WayPoints>();
        }

        private void Start()
        {
            var valueTuple = wayPoints.GetPositionRotationAtIndex(2);
            var some = wayPoints.ShowWayPoints;
        }
    }
}


using UnityEngine;
using WayPointGizmoTool;


[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(WayPoints))]
public class NpcInteractable: MonoBehaviour
{
    [Header("NPC Stand Positions")]
    public WayPoints wayPoints;
    [SerializeField] private Collider  _collider;

    public Collider AttachedCollider => _collider;

    private void OnValidate()
    {
        wayPoints ??= GetComponent<WayPoints>();
        _collider ??= GetComponent<Collider>();
    }
    
} // class 
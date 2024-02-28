#if UNITY_EDITOR

using TriInspector;
using UnityEngine;

internal class GizmoDrawer : MonoBehaviour
{
    private enum  GizmoTypes
    {
        None, Sphere, WireSphere, Cube, WireCube, WireMesh
    }
    
    private enum DrawTypes
    {
        DoNotDraw, Always, OnSelected
    }

    private bool _radiusNeeded;
    private bool _sizeNeeded;
    private bool _meshNeeded;
    
    private void OnGizmoTypeChanged()
    {
        switch (gizmoType)
        {
            case GizmoTypes.None:
                _radiusNeeded = false;
                _sizeNeeded = false;
                _meshNeeded = false;
                break;
            case GizmoTypes.Sphere:
                _radiusNeeded = true;
                _sizeNeeded = false;
                _meshNeeded = false;
                break;
            case GizmoTypes.WireSphere:
                _radiusNeeded = true;
                _sizeNeeded = false;
                _meshNeeded = false;
                break;
            case GizmoTypes.Cube:
                _radiusNeeded = false;
                _sizeNeeded = true;
                _meshNeeded = false;
                break;
            case GizmoTypes.WireCube:
                _radiusNeeded = false;
                _sizeNeeded = true;
                _meshNeeded = false;
                break;
            case GizmoTypes.WireMesh:
                _radiusNeeded = false;
                _sizeNeeded = false;
                _meshNeeded = true;
                break;
        }
    }
    
    [OnValueChanged(nameof(OnGizmoTypeChanged))]
    [SerializeField] private  GizmoTypes gizmoType;
    
    [DisableIf(nameof(gizmoType), GizmoTypes.None)]
    [SerializeField] private DrawTypes drawType;
    [DisableIf(nameof(gizmoType), GizmoTypes.None)]
    [SerializeField] private Color color = Color.red;

    [ShowIf(nameof(_radiusNeeded))]
    [SerializeField] private float radius = 0.3f;
    [ShowIf(nameof(_sizeNeeded))]
    [SerializeField] private float size = 0.3f;
    [ShowIf(nameof(_meshNeeded))][Required]
    [SerializeField] private Mesh mesh;
    
    [ShowIf(nameof(_meshNeeded))] [SerializeField]
    private Vector3 scale;

    private void OnDrawGizmos()
    {
        if (drawType == DrawTypes.DoNotDraw) return;
        if (drawType == DrawTypes.Always)
        {
            Gizmos.color = color;
            switch (gizmoType)
            {
                case GizmoTypes.Sphere:
                    Gizmos.DrawSphere(transform.position, radius);
                    break;
                case GizmoTypes.WireSphere:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case GizmoTypes.Cube:
                    Gizmos.DrawCube(transform.position, Vector3.one * size);
                    break;
                case GizmoTypes.WireCube:
                    Gizmos.DrawWireCube(transform.position, Vector3.one * size);
                    break;
                case GizmoTypes.WireMesh:
                    Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, scale);
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (drawType == DrawTypes.OnSelected)
        {
            Gizmos.color = color;
            switch (gizmoType)
            {
                case GizmoTypes.Sphere:
                    Gizmos.DrawSphere(transform.position, radius);
                    break;
                case GizmoTypes.WireSphere:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case GizmoTypes.Cube:
                    Gizmos.DrawCube(transform.position, Vector3.one * size);
                    break;
                case GizmoTypes.WireCube:
                    Gizmos.DrawWireCube(transform.position, Vector3.one * size);
                    break;
                case GizmoTypes.WireMesh:
                    Gizmos.DrawWireMesh(mesh, transform.position, transform.rotation, scale);
                    break;
            }
        }
    }
}

#endif


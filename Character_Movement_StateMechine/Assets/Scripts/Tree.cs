using UnityEngine;
using UnityEngine.Serialization;

public class Tree : MonoBehaviour
{
    [Header("Tree Property")]
    [Tooltip("The time it takes to cut the tree")]
    [SerializeField] private float cuttingTime = 10.0f;
    [SerializeField] private float health = 100.0f;
    [FormerlySerializedAs("npcInteractable")] public NpcIntractable npcIntractable;

    public float CuttingTime => cuttingTime;
    public float Health => health;
    private void OnValidate()
    {
        npcIntractable ??= GetComponent<NpcIntractable>();
    }
} // class

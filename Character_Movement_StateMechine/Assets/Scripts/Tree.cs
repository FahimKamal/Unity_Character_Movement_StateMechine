using UnityEngine;

public class Tree : NpcInteractable
{
    [Header("Tree Property")]
    [Tooltip("The time it takes to cut the tree")]
    [SerializeField] private float cuttingTime = 10.0f;
    [SerializeField] private float health = 100.0f;
    public float CuttingTime => cuttingTime;
    public float Health => health;
} // class

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    [RequireComponent(typeof(NpcIntractable))]
    public class Inventory : MonoBehaviour
    {
        [FormerlySerializedAs("npcInteractable")] public NpcIntractable npcIntractable;

        private void OnValidate()
        {
            npcIntractable ??= GetComponent<NpcIntractable>();
        }
    }
}


using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(NpcInteractable))]
    public class Inventory : MonoBehaviour
    {
        public NpcInteractable npcInteractable;

        private void OnValidate()
        {
            npcInteractable ??= GetComponent<NpcInteractable>();
        }
    }
}


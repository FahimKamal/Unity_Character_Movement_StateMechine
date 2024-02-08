
using UnityEngine;

[RequireComponent(typeof(NpcInteractable))]
public class Field : MonoBehaviour
{
    public NpcInteractable npcInteractable;

    private void OnValidate()
    {
        npcInteractable ??= GetComponent<NpcInteractable>();
    }
}

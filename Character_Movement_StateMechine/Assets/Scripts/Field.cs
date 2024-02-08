
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(NpcIntractable))]
public class Field : MonoBehaviour
{
    [FormerlySerializedAs("npcInteractable")] public NpcIntractable npcIntractable;

    private void OnValidate()
    {
        npcIntractable ??= GetComponent<NpcIntractable>();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WayPointParent))]
public class Inventory : NpcInteractable
{
    [SerializeField] private WayPointParent _wayPointParent;
    private void OnValidate()
    {
        _wayPointParent ??= GetComponent<WayPointParent>();
    }

    private void Start()
    {
        // WayPoints x = _wayPointParent;
    }
}

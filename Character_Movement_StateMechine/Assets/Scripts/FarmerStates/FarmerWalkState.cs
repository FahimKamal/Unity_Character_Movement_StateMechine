using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FarmerWalkState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Walk State");
        farmer.PlayAnimation(KeyManager.Walking);
        if (farmer.farmerAction == FarmerActions.Idle)
        {
            farmer.agent.SetDestination(farmer.wayPoints.WayPointList[Random.Range(0, farmer.wayPoints.WayPointList.Count)].position);
        }
        else
        {
            farmer.agent.SetDestination(farmer.FirstDestination);
        }
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        if (farmer.farmerAction == FarmerActions.Idle && farmer.agent.remainingDistance <= farmer.agent.stoppingDistance)
        {
            farmer.SwitchState(farmer.idleState);
        }
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Walk State");
    }

    public override void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        if (collider.CompareTag(KeyManager.TagInventory) && farmer.farmerAction is FarmerActions.Seeding)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.transform.GetChild(0).transform;
            
            StartCoroutine(GetToPositionAndRotation(
                targetTrans.position,
                targetTrans.rotation,
                0.3f,
                (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.seedingState);
                        }
                    }
                ));
            
            // farmer.SwitchState(farmer.boxPickupState);
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction is FarmerActions.Watering)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.transform.GetChild(0).transform;
            
            StartCoroutine(GetToPositionAndRotation(
                targetTrans.position,
                targetTrans.rotation,
                3f,
                (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.wateringState);
                        }
                    }
                ));
            
            // farmer.SwitchState(farmer.wateringState);
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction == FarmerActions.Harvesting)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.transform.GetChild(0).transform;
            
            StartCoroutine(GetToPositionAndRotation(
                targetTrans.position,
                targetTrans.rotation,
                0.3f,
                (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.harvestingState);
                        }
                    }
                ));
            
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction == FarmerActions.Building)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.transform.GetChild(0).transform;
            
            StartCoroutine(GetToPositionAndRotation(
                targetTrans.position,
                targetTrans.rotation,
                0.3f,
                (success) =>
                {
                    if (success)
                    {
                        farmer.SwitchState(farmer.kneelDownState);
                    }
                }));
            
            
        }
        if (collider.CompareTag(KeyManager.TagInventory) || collider.CompareTag(KeyManager.TagField) && farmer.farmerAction is FarmerActions.Carry)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.transform.GetChild(0).transform;
            
            StartCoroutine(GetToPositionAndRotation(
                targetTrans.position,
                targetTrans.rotation,
                0.3f,
                (success) =>
                {
                    if (success)
                    {
                        farmer.SwitchState(farmer.boxPickupState);
                    }
                }));
        }

        if (collider.CompareTag(KeyManager.TagTree) && farmer.farmerAction is FarmerActions.CuttingTree)
        {
            var closestStandPosition = collider.GetComponent<Tree>().ClosestStandPosition(transform.position);
            StartCoroutine(GetToPositionAndRotation(
                closestStandPosition.position, 
                closestStandPosition.rotation, 
                0.3f,
                (bool success) =>
                {
                    if (success)
                    {
                        farmer.SwitchState(farmer.cutTreeState);
                    }
                }));
            
            
        }
    }

    private IEnumerator GetToPositionAndRotation(Vector3 pos, Quaternion rot, float time, Action<bool> callBack)
    {
        float t = 0;
        var position = transform.position;
        var rotation = transform.rotation;
        while (t < time)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(position, pos, t / time);
            transform.rotation = Quaternion.Lerp(rotation, rot, t / time);
            yield return null;
        }
        callBack(true);
        yield break;
    }
    

} // class

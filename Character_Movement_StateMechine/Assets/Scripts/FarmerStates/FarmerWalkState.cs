using UnityEngine;

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
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.boxPickupState);
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction is FarmerActions.Watering)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.wateringState);
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction == FarmerActions.Harvesting)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.harvestingState);
        }
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction == FarmerActions.Building)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.kneelDownState);
        }
        if (collider.CompareTag(KeyManager.TagInventory) || collider.CompareTag(KeyManager.TagField) && farmer.farmerAction is FarmerActions.Carry)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.boxPickupState);
        }

        if (collider.CompareTag(KeyManager.TagTree) && farmer.farmerAction is FarmerActions.CuttingTree)
        {
            farmer.transform.position = collider.GetComponent<Tree>().ClosestStandPosition(transform.position).position;
            farmer.transform.rotation = collider.GetComponent<Tree>().ClosestStandPosition(transform.position).rotation;
            farmer.SwitchState(farmer.cutTreeState);
        }
    }
}

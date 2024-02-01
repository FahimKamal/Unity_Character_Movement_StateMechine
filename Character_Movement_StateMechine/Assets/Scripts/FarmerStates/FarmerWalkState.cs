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

        if (farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.agent.SetDestination(farmer.FirstDestination);
        }

        if (farmer.farmerAction == FarmerActions.Watering)
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
        if (collider.CompareTag("Inventory") && farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.transform.position = collider.transform.GetChild(0).transform.position;
            farmer.transform.rotation = collider.transform.GetChild(0).transform.rotation;
            farmer.SwitchState(farmer.boxPickupState);
        }

        if (collider.CompareTag("Field") && farmer.farmerAction == FarmerActions.Watering)
        {
            farmer.transform.position = collider.transform.GetChild(0).transform.position;
            farmer.transform.rotation = collider.transform.GetChild(0).transform.rotation;
            farmer.SwitchState(farmer.wateringState);
        }
    }
}

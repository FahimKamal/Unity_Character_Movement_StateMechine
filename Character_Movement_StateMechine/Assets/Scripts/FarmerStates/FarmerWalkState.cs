using UnityEngine;

public class FarmerWalkState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Walk State");
        farmer.animator.Play(KeyManager.Walking);
        if (farmer.farmerAction == FarmerActions.Idle)
        {
            farmer.agent.SetDestination(farmer.wayPoints.WayPointList[Random.Range(0, farmer.wayPoints.WayPointList.Count)].position);
        }

        if (farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.agent.SetDestination(farmer.FirstDestination);
        }
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        if (farmer.farmerAction == FarmerActions.Idle && farmer.agent.remainingDistance <= farmer.agent.stoppingDistance)
        {
            farmer.SwitchState(farmer.IdleState);
        }
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Walk State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        // if (collider.CompareTag("Inventory"))
        // {
            farmer.SwitchState(farmer.BoxPickupState);
        // }
    }
}

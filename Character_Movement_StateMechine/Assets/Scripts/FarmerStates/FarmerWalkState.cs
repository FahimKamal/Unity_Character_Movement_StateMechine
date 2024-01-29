using UnityEngine;

public class FarmerWalkState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Walk State");
        farmer.animator.Play("Walking");
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
        if (farmer.agent.remainingDistance <= farmer.agent.stoppingDistance)
        {
            if (farmer.farmerAction == FarmerActions.Idle)
            {
                farmer.SwitchState(farmer.IdleState);
            }

            if (farmer.farmerAction == FarmerActions.Seeding)
            {
                
            }
        }
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Walk State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

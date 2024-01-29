using UnityEngine;

public class FarmerWalkWithBoxState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering WalkWithBox State");
        farmer.agent.SetDestination(farmer.EndDestination);
        farmer.animator.Play("WalkingWithBox");
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        if (farmer.agent.remainingDistance <= farmer.agent.stoppingDistance && farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.SwitchState(farmer.SeedingState);
        }
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting WalkWithBox State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

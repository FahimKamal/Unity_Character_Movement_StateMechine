using UnityEngine;

public class FarmerSeedingState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Seeding State");
        // Play Seeding animation. 
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        // check if animation playing done or not. if Done action is done.
        farmer.ActionComplete();
        farmer.SwitchState(farmer.IdleState);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting seeding State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

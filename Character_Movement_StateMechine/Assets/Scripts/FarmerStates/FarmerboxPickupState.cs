using UnityEngine;

public class FarmerboxPickupState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering box pickup state");
        // Play box pickup animation. 
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        // Check if playing animation is over or not. if over switch to walk with box state.
        farmer.SwitchState(farmer.WalkWithBoxState);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting box pickup state.");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        throw new System.NotImplementedException();
    }
}

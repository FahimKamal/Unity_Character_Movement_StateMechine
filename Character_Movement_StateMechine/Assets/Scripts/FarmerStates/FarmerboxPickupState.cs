using UnityEngine;

public class FarmerBoxPickupState : FarmerBaseState
{
    private FarmerStateManager _manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering box pickup state");
        _manager = farmer;
        farmer.PlayAnimation(KeyManager.SeedPickingUp);
        // Play box pickup animation. 
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting box pickup state.");
    }

    public void PickupComplete()
    {
        Debug.Log("pickup complete");
        _manager.SwitchState(_manager.walkWithBoxState);
    }
}

using UnityEngine;

public class FarmerboxPickupState : FarmerBaseState
{
    private FarmerStateManager manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering box pickup state");
        manager = farmer;
        farmer.animator.Play(KeyManager.SeedPickingUp);
        // Play box pickup animation. 
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting box pickup state.");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

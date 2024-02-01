using UnityEngine;

public class FarmerSeedingState : FarmerBaseState
{
    private FarmerStateManager _manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Seeding State");
        _manager = farmer;
        // Play Seeding animation. 
        farmer.PlayAnimation(KeyManager.Seeding);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting seeding State");
    }

    public void SeedingComplete()
    {
        _manager.SwitchState(_manager.standUpState);
    }
}

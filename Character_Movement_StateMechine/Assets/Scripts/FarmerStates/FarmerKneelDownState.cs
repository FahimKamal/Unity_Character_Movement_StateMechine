using UnityEngine;

public class FarmerKneelDownState : FarmerBaseState
{
    private FarmerStateManager _manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Kneel Down State");
        _manager = farmer;
        farmer.PlayAnimation(KeyManager.KneelingDown);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Kneel Down State");
    }

    public void KneelDownComplete()
    {
        _manager.SwitchState(_manager.seedingState);
    }
}

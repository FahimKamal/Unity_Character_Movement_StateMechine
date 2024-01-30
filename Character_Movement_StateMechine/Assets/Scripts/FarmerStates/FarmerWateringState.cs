using UnityEngine;

public class FarmerWateringState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Watering State");
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Watering State");
    }
}

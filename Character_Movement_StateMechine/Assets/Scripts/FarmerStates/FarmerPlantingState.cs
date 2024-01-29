using UnityEngine;

public class FarmerPlantingState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Planting State");
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Planting State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

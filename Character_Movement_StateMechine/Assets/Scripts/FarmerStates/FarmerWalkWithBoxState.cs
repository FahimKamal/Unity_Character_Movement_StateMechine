using UnityEngine;

public class FarmerWalkWithBoxState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering WalkWithBox State");
        farmer.agent.SetDestination(farmer.EndDestination);
        farmer.PlayAnimation(KeyManager.WalkingWithBox);
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting WalkWithBox State");
    }

    public override void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        if (collider.CompareTag(KeyManager.TagField) && farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.kneelDownState);
            return;
        }

        if (collider.CompareTag(KeyManager.TagInventory) && farmer.farmerAction == FarmerActions.Harvesting)
        {
            farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            farmer.SwitchState(farmer.boxDropDownState);
        }
    }
}

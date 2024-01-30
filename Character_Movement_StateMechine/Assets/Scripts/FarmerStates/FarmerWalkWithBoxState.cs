using UnityEngine;

public class FarmerWalkWithBoxState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering WalkWithBox State");
        farmer.agent.SetDestination(farmer.EndDestination);
        farmer.animator.Play(KeyManager.WalkingWithBox);
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
        if (collider.CompareTag("Field") && farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.transform.position = collider.transform.GetChild(0).transform.position;
            farmer.transform.rotation = collider.transform.GetChild(0).transform.rotation;
            farmer.SwitchState(farmer.kneelDownState);
        }
    }
}

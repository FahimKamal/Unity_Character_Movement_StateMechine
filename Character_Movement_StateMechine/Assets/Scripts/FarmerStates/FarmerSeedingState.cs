using UnityEngine;

public class FarmerSeedingState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Seeding State");
        // Play Seeding animation. 
        farmer.animator.Play(KeyManager.Seeding);
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting seeding State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

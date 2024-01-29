using UnityEngine;

public class FarmerIdleState : FarmerBaseState
{
    private float _timer;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Idle State");
        farmer.animator.Play("Idle");
        _timer = 0;
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        if (farmer.farmerAction == FarmerActions.Idle)
        {
            _timer += Time.deltaTime;
            if (_timer > Random.Range(farmer.MinStandTime, farmer.MaxStandTime))
            {
                farmer.SwitchState(farmer.WalkingState);
            }
        }

        if (farmer.farmerAction == FarmerActions.Seeding)
        {
            farmer.SwitchState(farmer.WalkingState);
        }
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        _timer = 0;
        Debug.Log("Exiting Idle State");
    }

    public override void OnTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        
    }
}

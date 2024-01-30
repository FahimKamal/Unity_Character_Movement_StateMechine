using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerStandUpState : FarmerBaseState
{
    private FarmerStateManager _manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering StandUp State");
        _manager = farmer;
        farmer.animator.Play(KeyManager.StandUp);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting StandUp State");
    }

    public void StandUpComplete()
    {
        _manager.ActionComplete();
        _manager.SwitchState(_manager.idleState);
    }
}

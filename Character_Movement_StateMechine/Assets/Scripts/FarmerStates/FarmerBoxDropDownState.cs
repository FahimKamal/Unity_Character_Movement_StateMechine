using FarmerStates;
using UnityEngine;

public class FarmerBoxDropDownState : FarmerBaseState
{
    private FarmerStateManager _manager;
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Dropbox State");
        _manager = farmer;
        farmer.PlayAnimation(KeyManager.DropDown);
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Dropbox State");
    }

    /// <summary>
    /// Animation event method.
    /// </summary>
    public void DropDownComplete()
    {
        _manager.PlayAnimation(KeyManager.DropUp);
    }

    /// <summary>
    /// Animation event method.
    /// </summary>
    public void DropUpComplete()
    {
        _manager.ActionComplete();
    }
}

using UnityEngine;

public class FarmerBuildingState : FarmerBaseState
{
    public float buildingTime = 5.0f;
    private float _timer = 0.0f;
    private bool _isBuildingComplete = false;
    private FarmerStateManager _manager;

    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering Building State");
        _manager = farmer;
        farmer.PlayAnimation(KeyManager.Building);
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        if (_isBuildingComplete)
        {
            return;
        }
        _timer += Time.deltaTime;
        if (_timer >= buildingTime)
        {
            _isBuildingComplete = true;
        }
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting Building State");
        _isBuildingComplete = false;
        _timer = 0.0f;
    }

    public void BuildingComplete()
    {
        if (_isBuildingComplete)
        {
            _manager.SwitchState(_manager.standUpState);
        }
    }
}

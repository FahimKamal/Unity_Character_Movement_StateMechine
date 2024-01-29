using UnityEngine;

public abstract class FarmerBaseState
{
    public abstract  void EnterState(FarmerStateManager farmer);
    public abstract void UpdateState(FarmerStateManager farmer);
    public abstract void ExitState(FarmerStateManager farmer);
    public abstract void OnTriggerEnter(FarmerStateManager farmer, Collider collider);
}

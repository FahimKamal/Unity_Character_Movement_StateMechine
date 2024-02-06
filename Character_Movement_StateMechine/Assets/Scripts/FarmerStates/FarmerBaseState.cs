using UnityEngine;

namespace FarmerStates
{
    public abstract class FarmerBaseState : MonoBehaviour
    {
        public abstract  void EnterState(FarmerStateManager farmer);

        public virtual void UpdateState(FarmerStateManager farmer) { }

        public virtual void ExitState(FarmerStateManager farmer) { }

        public virtual void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider) { }
    }
}

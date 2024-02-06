using UnityEngine;

namespace FarmerStates
{
    public class FarmerStandUpState : FarmerBaseState
    {
        private FarmerStateManager _manager;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering StandUp State");
            _manager = farmer;
            farmer.PlayAnimation(KeyManager.StandUp);
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting StandUp State");
        }

        /// <summary>
        /// Animation event method.
        /// </summary>
        public void StandUpComplete()
        {
            _manager.ActionComplete();
        }
    }
}

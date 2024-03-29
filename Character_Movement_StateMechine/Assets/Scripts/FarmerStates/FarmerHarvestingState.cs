using UnityEngine;

namespace FarmerStates
{
    public class FarmerHarvestingState : FarmerBaseState
    {
        private FarmerStateManager _manager;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Harvesting State");
            _manager = farmer;
            farmer.PlayAnimation(KeyManager.PickFruit);
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting Harvesting State");
        }

        /// <summary>
        /// Animation event method.
        /// </summary>
        public void HarvestingComplete()
        {
            _manager.SwitchState(_manager.walkWithBoxState);
        }
    }
}

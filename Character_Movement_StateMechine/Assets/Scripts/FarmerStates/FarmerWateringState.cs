using UnityEngine;

namespace FarmerStates
{
    public class FarmerWateringState : FarmerBaseState
    {
        private FarmerStateManager _manager;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Watering State");
            _manager = farmer;
            farmer.PlayAnimation(KeyManager.Watering);
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting Watering State");
        }

        /// <summary>
        /// Animation event method.
        /// </summary>
        public void WateringComplete()
        {
            Debug.Log("Watering Complete");
            _manager.ActionComplete();
        
        }
    }
}

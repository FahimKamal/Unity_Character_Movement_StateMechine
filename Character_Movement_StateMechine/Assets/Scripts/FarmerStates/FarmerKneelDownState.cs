using UnityEngine;

namespace FarmerStates
{
    public class FarmerKneelDownState : FarmerBaseState
    {
        private FarmerStateManager _manager;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Kneel Down State");
            _manager = farmer;
            farmer.PlayAnimation(KeyManager.KneelingDown);
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting Kneel Down State");
        }

        /// <summary>
        /// Animation event method.
        /// </summary>
        public void KneelDownComplete()
        {
            if (_manager.farmerAction == FarmerActions.Seeding)
            {
                _manager.SwitchState(_manager.seedingState);
            }

            if (_manager.farmerAction == FarmerActions.Building)
            {
                _manager.SwitchState(_manager.buildingState);
            }
        }
    }
}

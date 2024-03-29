using UnityEngine;

namespace FarmerStates
{
    public class FarmerIdleState : FarmerBaseState
    {
        private float _timer;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Idle State");
            if (farmer.farmerAction == FarmerActions.Idle)
            {
                farmer.PlayAnimation(KeyManager.Idle);
            }
            else 
            {
                farmer.SwitchState(farmer.walkingState);
            }
        }

        public override void UpdateState(FarmerStateManager farmer)
        {
            if (farmer.farmerAction == FarmerActions.Idle)
            {
                _timer += Time.deltaTime;
                if (_timer > Random.Range(farmer.MinStandTime, farmer.MaxStandTime))
                {
                    farmer.SwitchState(farmer.walkingState);
                }
            }
        
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            _timer = 0;
            Debug.Log("Exiting Idle State");
        }
    }
}

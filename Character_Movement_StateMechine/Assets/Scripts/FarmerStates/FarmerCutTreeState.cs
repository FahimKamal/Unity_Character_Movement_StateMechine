using UnityEngine;

namespace FarmerStates
{
    public class FarmerCutTreeState : FarmerBaseState
    {
        public float cutTreeTime = 5.0f;
        public float cutTreeTimer = 0.0f;
        private bool _isCuttingComplete = false;
        private FarmerStateManager _manager;
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Cutting Tree State");
            _manager = farmer;
            farmer.PlayAnimation(KeyManager.CutTree);
        }

        public override void UpdateState(FarmerStateManager farmer)
        {
            if (_isCuttingComplete)
            {
                return;
            }
            cutTreeTimer += Time.deltaTime;
            if (cutTreeTimer >= cutTreeTime)
            {
                _isCuttingComplete = true;
            }
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting Cutting Tree State");
            _isCuttingComplete = false;
            cutTreeTimer = 0.0f;
        }

        /// <summary>
        /// Animation Event Method. 
        /// </summary>
        public void AexSwingComplete()
        {
            if (_isCuttingComplete)
            {
                _manager.SwitchState(_manager.walkWithBoxState);
            }
        }
    }
}

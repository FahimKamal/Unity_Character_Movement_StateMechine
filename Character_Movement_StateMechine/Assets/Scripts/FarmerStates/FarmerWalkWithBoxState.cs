using UnityEngine;

namespace FarmerStates
{
    public class FarmerWalkWithBoxState : FarmerBaseState
    {
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering WalkWithBox State");
            farmer.agent.SetDestination(farmer.EndDestination.wayPoints.GetClosestPositionRotation(transform.position).position);
            farmer.PlayAnimation(KeyManager.WalkingWithBox);
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting WalkWithBox State");
        }

        public override void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider)
        {
            if (farmer.farmerAction == FarmerActions.Seeding && collider == farmer.EndDestination.AttachedCollider)
            {
                var targetTrans = collider.GetComponent<NpcIntractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.kneelDownState);
                        }
                    }
                ));
            
                return;
            }

            if ((farmer.farmerAction is FarmerActions.Harvesting or FarmerActions.CuttingTree) && collider == farmer.EndDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcIntractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.boxDropDownState);
                        }
                    }
                ));
            
            }

            if (farmer.farmerAction is FarmerActions.Carry && collider == farmer.EndDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcIntractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.boxDropDownState);
                        }
                    }
                ));
            }
        }
    }
}

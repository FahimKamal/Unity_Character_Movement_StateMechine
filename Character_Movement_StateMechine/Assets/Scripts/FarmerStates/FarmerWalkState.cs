using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmerStates
{
    public class FarmerWalkState : FarmerBaseState
    {
        public override void EnterState(FarmerStateManager farmer)
        {
            Debug.Log("Entering Walk State");
            farmer.PlayAnimation(KeyManager.Walking);
            if (farmer.farmerAction == FarmerActions.Idle)
            {
                farmer.agent.SetDestination(farmer.wayPoints.WayPointList[Random.Range(0, farmer.wayPoints.WayPointList.Count)].position);
            }
            else
            {
                farmer.agent.SetDestination(farmer.FirstDestination.wayPoints.GetClosestPositionRotation(transform.position).position);
            }
        }

        public override void UpdateState(FarmerStateManager farmer)
        {
            if (farmer.farmerAction == FarmerActions.Idle && farmer.agent.remainingDistance <= farmer.agent.stoppingDistance)
            {
                farmer.SwitchState(farmer.idleState);
            }
        }

        public override void ExitState(FarmerStateManager farmer)
        {
            Debug.Log("Exiting Walk State");
        }

    
        public override void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider)
        {
            if (farmer.farmerAction is FarmerActions.Seeding && collider == farmer.FirstDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.boxPickupState);
                        }
                    }
                ));
            
                // farmer.SwitchState(farmer.boxPickupState);
            }
            if (farmer.farmerAction is FarmerActions.Watering && collider == farmer.FirstDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.wateringState);
                        }
                    }
                ));
            
                // farmer.SwitchState(farmer.wateringState);
            }
            if (farmer.farmerAction == FarmerActions.Harvesting && collider == farmer.FirstDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.harvestingState);
                        }
                    }
                ));
            
            }
            if (farmer.farmerAction == FarmerActions.Building && collider == farmer.FirstDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
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
                    }));
            
            
            }
            if (farmer.farmerAction is FarmerActions.Carry && collider == farmer.FirstDestination.AttachedCollider)
            {
                // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
                var targetTrans = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
            
                StartCoroutine(farmer.GetToPositionAndRotation(
                    targetTrans.position,
                    targetTrans.rotation,
                    0.3f,
                    (success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.boxPickupState);
                        }
                    }));
            }

            if (farmer.farmerAction is FarmerActions.CuttingTree && collider == farmer.FirstDestination.AttachedCollider)
            {
                var closestStandPosition = collider.GetComponent<NpcInteractable>().wayPoints.GetClosestPositionRotation(transform.position);
                StartCoroutine(farmer.GetToPositionAndRotation(
                    closestStandPosition.position, 
                    closestStandPosition.rotation, 
                    0.3f,
                    (bool success) =>
                    {
                        if (success)
                        {
                            farmer.SwitchState(farmer.cutTreeState);
                        }
                    }));
            
            
            }
        }
    

    }
} // class

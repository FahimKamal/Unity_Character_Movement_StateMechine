using FarmerStates;
using UnityEngine;

public class FarmerWalkWithBoxState : FarmerBaseState
{
    public override void EnterState(FarmerStateManager farmer)
    {
        Debug.Log("Entering WalkWithBox State");
        farmer.agent.SetDestination(farmer.EndDestination.ClosestStandPositionAndRotation(transform.position).position);
        farmer.PlayAnimation(KeyManager.WalkingWithBox);
    }

    public override void UpdateState(FarmerStateManager farmer)
    {
        
    }

    public override void ExitState(FarmerStateManager farmer)
    {
        Debug.Log("Exiting WalkWithBox State");
    }

    public override void OnStateTriggerEnter(FarmerStateManager farmer, Collider collider)
    {
        if (collider == farmer.EndDestination.AttachedCollider && farmer.farmerAction == FarmerActions.Seeding)
        {
            var targetTrans = collider.GetComponent<NpcInteractable>().ClosestStandPositionAndRotation(transform.position);
            
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

        if (collider == farmer.EndDestination.AttachedCollider && (farmer.farmerAction is FarmerActions.Harvesting or FarmerActions.CuttingTree))
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.GetComponent<NpcInteractable>().ClosestStandPositionAndRotation(transform.position);
            
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

        if (collider == farmer.EndDestination.AttachedCollider && farmer.farmerAction is FarmerActions.Carry)
        {
            // farmer.SetPositionAndRotation(collider.transform.GetChild(0).transform);
            var targetTrans = collider.GetComponent<NpcInteractable>().ClosestStandPositionAndRotation(transform.position);
            
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

using UnityEngine;
using UnityEngine.AI;

public class FarmerStateManager : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float maxStandTime = 7f;
    [SerializeField] private float minStandTime = 5f;
    [SerializeField] private float maxWalkTime = 19f;
    [SerializeField] private float minWalkTime = 9f;
    
    public float MaxStandTime => maxStandTime;
    public float MinStandTime => minStandTime;
    public float MaxWalkTime => maxWalkTime;
    public float MinWalkTime => minWalkTime;
    
    public FarmerBaseState CurrentState;
    public FarmerIdleState IdleState = new FarmerIdleState();
    public FarmerWalkState WalkingState = new FarmerWalkState();
    public FarmerWalkWithBoxState WalkWithBoxState = new FarmerWalkWithBoxState();
    public FarmerHarvestingState HarvestingState = new FarmerHarvestingState();
    public FarmerWateringState WateringState = new FarmerWateringState();
    public FarmerPlantingState PlantingState = new FarmerPlantingState();

    public NavMeshAgent agent;
    public Animator animator;

    public AiWayPoints wayPoints;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        CurrentState = IdleState;
        CurrentState.EnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(FarmerBaseState state)
    {
        CurrentState.ExitState(this);
        CurrentState = state;
        CurrentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(this, other);
    }
}

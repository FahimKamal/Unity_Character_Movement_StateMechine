using TriInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public enum FarmerActions
{
    Idle,
    Seeding,
    Watering,
    Harvesting,
}
public class FarmerStateManager : MonoBehaviour
{
    public FarmerActions farmerAction;
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
    public FarmerboxPickupState BoxPickupState = new FarmerboxPickupState();
    public FarmerSeedingState SeedingState = new FarmerSeedingState();

    public NavMeshAgent agent;
    public Animator animator;
    public AiWayPoints wayPoints;
    
    private Vector3 _firstDestination;
    private Vector3 _endDestination;
    public Vector3 FirstDestination => _firstDestination;
    public Vector3 EndDestination => _endDestination;

    public Transform Field;
    public Transform Inventory;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        _firstDestination = Inventory.position;
        _endDestination = Field.position;

        farmerAction = FarmerActions.Idle;
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
    
    [Button]
    public void GoSeeding(Vector3 firstDestination, Vector3 endDestination)
    {
        // isBusy = true;
        // seeding = true;
        farmerAction = FarmerActions.Seeding;
        // _firstDestination = firstDestination;
        // _endDestination = endDestination;
        SwitchState(IdleState);
    }

    public void GoWatering(Vector3 firstDestination, Vector3 endDestination)
    {
        // isBusy = true;
        // watering = true;
        farmerAction = FarmerActions.Watering;
        _firstDestination = firstDestination;
        _endDestination = endDestination;
    }

    public void GoHarvesting(Vector3 firstDestination, Vector3 endDestination)
    {
        // isBusy = true;
        // harvesting = true;
        farmerAction = FarmerActions.Harvesting;
        _firstDestination = firstDestination;
        _endDestination = endDestination;
    }

    [Button]
    public void ActionComplete()
    {
        farmerAction = FarmerActions.Idle;
        _firstDestination = Vector3.zero;
        _endDestination = Vector3.zero;
    }
}

using TriInspector;
using UnityEngine;
using UnityEngine.AI;

public enum FarmerActions
{
    Idle, Seeding, Watering, Harvesting, Building, Carry, CuttingTree
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

    private FarmerBaseState _currentState;
    [HideInInspector] public FarmerIdleState        idleState;
    [HideInInspector] public FarmerWalkState        walkingState;
    [HideInInspector] public FarmerBoxPickupState   boxPickupState; 
    [HideInInspector] public FarmerWalkWithBoxState walkWithBoxState;
    [HideInInspector] public FarmerKneelDownState   kneelDownState;
    [HideInInspector] public FarmerSeedingState     seedingState;
    [HideInInspector] public FarmerStandUpState     standUpState;
    [HideInInspector] public FarmerHarvestingState  harvestingState;
    [HideInInspector] public FarmerWateringState    wateringState;
    [HideInInspector] public FarmerBoxDropDownState boxDropDownState;
    [HideInInspector] public FarmerBuildingState    buildingState;

    public NavMeshAgent agent;
    public Animator     animator;
    public AiWayPoints  wayPoints;
    
    private Vector3 _firstDestination;
    private Vector3 _endDestination;
    public Vector3 FirstDestination => _firstDestination;
    public Vector3 EndDestination   => _endDestination;

    public Transform field;
    public Transform inventory;
    
    private  bool _isBusy;
    [SerializeField] private string _currentAnimState = KeyManager.Idle;

    // Start is called before the first frame update
    private void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        idleState        = GetComponent<FarmerIdleState>();
        walkingState     = GetComponent<FarmerWalkState>();
        boxPickupState   = GetComponent<FarmerBoxPickupState>();
        walkWithBoxState = GetComponent<FarmerWalkWithBoxState>();
        kneelDownState   = GetComponent<FarmerKneelDownState>();
        seedingState     = GetComponent<FarmerSeedingState>();
        standUpState     = GetComponent<FarmerStandUpState>();
        wateringState    = GetComponent<FarmerWateringState>();
        harvestingState  = GetComponent<FarmerHarvestingState>();
        boxDropDownState = GetComponent<FarmerBoxDropDownState>();
        buildingState    = GetComponent<FarmerBuildingState>();


        _firstDestination = inventory.position;
        _endDestination   = field.position;

        farmerAction  = FarmerActions.Idle;
        _currentState = idleState;
        _currentState.EnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnStateTriggerEnter(this, other);
    }

    public void SwitchState(FarmerBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    [Button]
    public bool GoSeeding(Vector3 firstDestination, Vector3 endDestination)
    {
        if (_isBusy)
            return false;
        
        _isBusy = true;
        farmerAction = FarmerActions.Seeding;
        // _firstDestination = firstDestination;
        // _endDestination = endDestination;
        _firstDestination = inventory.position;
        _endDestination = field.position;
        SwitchState(idleState);
        return true;
    }

    [Button]
    public bool GoWatering(Vector3 firstDestination)
    {
        if (_isBusy)
            return false;
        
        _isBusy = true;
        farmerAction = FarmerActions.Watering;
        // _firstDestination = firstDestination;
        _firstDestination = field.position;
        SwitchState(idleState);
        
        return true;
    }

    [Button]
    public bool GoHarvesting(Vector3 firstDestination, Vector3 endDestination)
    {
        if (_isBusy)
            return false;
        
        _isBusy = true;
        farmerAction = FarmerActions.Harvesting;
        // _firstDestination = firstDestination;
        // _endDestination = endDestination;
        _firstDestination = field.position;
        _endDestination = inventory.position;
        SwitchState(idleState);

        return true;
    }

    [Button]
    public bool GoBuilding(Vector3 firstDestination, float buildingTime)
    {
        if (_isBusy)
            return false;

        _isBusy = true;
        farmerAction = FarmerActions.Building;
        buildingState.buildingTime = buildingTime;
        // _firstDestination = firstDestination;
        _firstDestination = field.position;
        SwitchState(idleState);

        return true; 
    }

    [Button]
    public bool GoCarry(Vector3 firstDestination, Vector3 endDestination)
    {
        if (_isBusy)
            return false;

        _isBusy = true;
        farmerAction = FarmerActions.Carry;
        // _firstDestination = firstDestination;
        // _endDestination = endDestination;
        _firstDestination = inventory.position;
        _endDestination = field.position;
        SwitchState(idleState);

        return true; 
    }
    
    

    
    public void PlayAnimation(string newState)
    {
        if (_currentAnimState == newState) return;
        
        _currentAnimState = newState;
        animator.CrossFade(newState, 0.08f);
    }

    [Button]
    public void ActionComplete()
    {
        _isBusy = false;
        farmerAction = FarmerActions.Idle;
        _firstDestination = Vector3.zero;
        _endDestination   = Vector3.zero;
        SwitchState(walkingState);
    }

    public void SetPositionAndRotation(Transform target)
    {
        transform.SetPositionAndRotation(target.position, target.rotation);
    }
}
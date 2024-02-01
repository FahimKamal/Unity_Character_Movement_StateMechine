using TriInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
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

    private FarmerBaseState _currentState;
    [HideInInspector] public FarmerIdleState idleState;
    [HideInInspector] public FarmerWalkState walkingState;
    [HideInInspector] public FarmerBoxPickupState boxPickupState; 
    [HideInInspector] public FarmerWalkWithBoxState walkWithBoxState;
    [HideInInspector] public FarmerKneelDownState kneelDownState;
    [HideInInspector] public FarmerSeedingState seedingState;
    [HideInInspector] public FarmerStandUpState standUpState;
    [HideInInspector] public FarmerHarvestingState harvestingState;
    [HideInInspector] public FarmerWateringState wateringState;

    public NavMeshAgent agent;
    public Animator animator;
    public AiWayPoints wayPoints;
    
    private Vector3 _firstDestination;
    private Vector3 _endDestination;
    public Vector3 FirstDestination => _firstDestination;
    public Vector3 EndDestination => _endDestination;

    public Transform field;
    public Transform inventory;
    
    private  bool _isBusy;

    // Start is called before the first frame update
    private void Start()
    {
        idleState = GetComponent<FarmerIdleState>();
        walkingState = GetComponent<FarmerWalkState>();
        boxPickupState = GetComponent<FarmerBoxPickupState>();
        walkWithBoxState = GetComponent<FarmerWalkWithBoxState>();
        kneelDownState = GetComponent<FarmerKneelDownState>();
        seedingState = GetComponent<FarmerSeedingState>();
        standUpState = GetComponent<FarmerStandUpState>();
        wateringState = GetComponent<FarmerWateringState>();
        harvestingState = GetComponent<FarmerHarvestingState>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        _firstDestination = inventory.position;
        _endDestination = field.position;

        farmerAction = FarmerActions.Idle;
        _currentState = idleState;
        _currentState.EnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(FarmerBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentState.OnStateTriggerEnter(this, other);
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
    public bool GoWatering(Vector3 firstDestination, Vector3 endDestination)
    {
        if (_isBusy)
            return false;
        
        _isBusy = true;
        farmerAction = FarmerActions.Watering;
        // _firstDestination = firstDestination;
        // _endDestination = endDestination;
        // _firstDestination = inventory.position;
        _firstDestination = field.position;
        SwitchState(idleState);
        
        return true;
    }

    public bool GoHarvesting(Vector3 firstDestination, Vector3 endDestination)
    {
        if (_isBusy)
            return false;
        
        _isBusy = true;
        farmerAction = FarmerActions.Harvesting;
        _firstDestination = firstDestination;
        _endDestination = endDestination;

        return true;
    }

    private string _currentAnimState = KeyManager.Idle;
    public void PlayAnimation(string newState)
    {
        if (_currentAnimState == newState)
        {
            return;
        }
        _currentAnimState = newState;
        animator.CrossFade(newState, 0.05f);
        // animator.Play(animationName);
    }

    [Button]
    public void ActionComplete()
    {
        _isBusy = false;
        farmerAction = FarmerActions.Idle;
        _firstDestination = Vector3.zero;
        _endDestination = Vector3.zero;
    }
}
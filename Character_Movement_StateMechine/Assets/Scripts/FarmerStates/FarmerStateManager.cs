using System;
using System.Collections;
using TriInspector;
using UnityEngine;
using UnityEngine.AI;

namespace FarmerStates
{
    /// <summary>
    /// Enumeration defining various actions that a farmer can perform.
    /// </summary>
    public enum FarmerActions
    {
        Idle, Seeding, Watering, Harvesting, Building, Carry, CuttingTree
    }

    /// <summary>
    /// Manager class for controlling the state and behavior of a farmer character.
    /// </summary>
    public class FarmerStateManager : MonoBehaviour
    {
        [SerializeField] internal FarmerActions farmerAction;
        [Header("Idle")]
        [SerializeField] private float maxStandTime = 7f;
        [SerializeField] private float minStandTime = 5f;
        [SerializeField] private float maxWalkTime = 19f;
        [SerializeField] private float minWalkTime = 9f;
    
        // Properties for accessing idle and walk time ranges.
        public float MaxStandTime => maxStandTime;
        public float MinStandTime => minStandTime;
        public float MaxWalkTime => maxWalkTime;
        public float MinWalkTime => minWalkTime;

        // References to different states of the farmer.
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
        [HideInInspector] public FarmerCutTreeState     cutTreeState;

        public NavMeshAgent agent;
        public Animator     animator;
        public AiWayPoints  wayPoints;
    
        private NpcInteractable _firstDestination;
        private NpcInteractable _endDestination;
        public NpcInteractable FirstDestination => _firstDestination;
        public NpcInteractable EndDestination   => _endDestination;

        public NpcInteractable field;
        public NpcInteractable inventory;
    
        private  bool _isBusy;
        [SerializeField] private string currentAnimState = KeyManager.Idle;

    
        private void Start()
        {
            // Component references and initializations.
            agent    = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            // Getting references to different states.
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
            cutTreeState     = GetComponent<FarmerCutTreeState>();

            // Initial positions for inventory and field destinations.
            _firstDestination = inventory;
            _endDestination   = field;

            // Initial state and entering the state.
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

        /// <summary>
        /// Exit the current state and enter the next state that is given. 
        /// </summary>
        /// <param name="state">Next state to start.</param>
        public void SwitchState(FarmerBaseState state)
        {
            _currentState.ExitState(this);
            _currentState = state;
            _currentState.EnterState(this);
        }

        /// <summary>
        /// Give the NPC seeding command. NPC will go to first destination to pick up seed and carry it
        /// to the end destination which is a field and play seeding animation.
        /// Returns a bool True as the status NPC is started executing the given command or False if NPC is
        /// busy already executing a task.  
        /// </summary>
        /// <param name="firstDestination">Inventory Location.</param>
        /// <param name="endDestination">Field Location</param>
        /// <returns>Bool status of command execution</returns>
        [Button]
        public bool GoSeeding(NpcInteractable firstDestination, NpcInteractable endDestination)
        {
            if (_isBusy)
                return false;
        
            _isBusy = true;
            farmerAction = FarmerActions.Seeding;
            // _firstDestination = firstDestination;
            // _endDestination = endDestination;
            _firstDestination = inventory;
            _endDestination = field;
            SwitchState(idleState);
            return true;
        }

        /// <summary>
        /// Give NPC the command to go to watering a field. 
        /// </summary>
        /// <param name="firstDestination">Location of the field.</param>
        /// <returns>Bool status of command execution</returns>
        [Button]
        public bool GoWatering(NpcInteractable firstDestination)
        {
            if (_isBusy)
                return false;
        
            _isBusy = true;
            farmerAction = FarmerActions.Watering;
            // _firstDestination = firstDestination;
            _firstDestination = field;
            SwitchState(idleState);
        
            return true;
        }

        /// <summary>
        /// Give the command to harvest crop from a field and carry it to the inventory. 
        /// </summary>
        /// <param name="firstDestination">Location of the field.</param>
        /// <param name="endDestination">Location of the inventory.</param>
        /// <returns>Bool status of command execution.</returns>
        [Button]
        public bool GoHarvesting(NpcInteractable firstDestination, NpcInteractable endDestination)
        {
            if (_isBusy)
                return false;
        
            _isBusy = true;
            farmerAction = FarmerActions.Harvesting;
            // _firstDestination = firstDestination;
            // _endDestination = endDestination;
            _firstDestination = field;
            _endDestination = inventory;
            SwitchState(idleState);

            return true;
        }

        /// <summary>
        /// Give command to go to a location and build something. 
        /// </summary>
        /// <param name="firstDestination">Location to build something</param>
        /// <param name="buildingTime">Time needed to complete the task.</param>
        /// <returns>Bool status of command execution.</returns>
        [Button]
        public bool GoBuilding(NpcInteractable firstDestination, float buildingTime)
        {
            if (_isBusy)
                return false;

            _isBusy = true;
            farmerAction = FarmerActions.Building;
            buildingState.buildingTime = buildingTime;
            // _firstDestination = firstDestination;
            _firstDestination = field;
            SwitchState(idleState);

            return true; 
        }

        /// <summary>
        /// Carry something from first location to second.
        /// </summary>
        /// <param name="firstDestination">Location to pick up object</param>
        /// <param name="endDestination">Location to drop object</param>
        /// <returns>Bool status of command execution.</returns>
        [Button]
        public bool GoCarry(NpcInteractable firstDestination, NpcInteractable endDestination)
        {
            if (_isBusy)
                return false;

            _isBusy = true;
            farmerAction = FarmerActions.Carry;
            // _firstDestination = firstDestination;
            // _endDestination = endDestination;
            _firstDestination = inventory;
            _endDestination = field;
            SwitchState(idleState);

            return true; 
        }

        /// <summary>
        /// Give command to go to a tree location to cut that and bring the wood to the inventory.
        /// </summary>
        /// <param name="tree">The specific tree to cut.</param>
        /// <param name="endDestination">Inventory location to bring the wood.</param>
        /// <returns>Bool status of command execution.</returns>
        [Button]
        public bool GoCutTree(Tree tree, NpcInteractable endDestination)
        {
            if (_isBusy)
                return false;

            _isBusy = true;
            farmerAction = FarmerActions.CuttingTree;
            cutTreeState.cutTreeTime = tree.CuttingTime;
            _firstDestination = tree;
            _endDestination = inventory;
            SwitchState(idleState);

            return true;
        }

    
        /// <summary>
        /// Internal method to play animation by the farmer animator component. 
        /// </summary>
        /// <param name="newState">Next animation state to play.</param>
        internal void PlayAnimation(string newState)
        {
            // If the given animation state is already playing, do nothing. 
            if (currentAnimState == newState) return;
        
            currentAnimState = newState;
            animator.CrossFade(newState, 0.08f);
        }

        /// <summary>
        /// Internal method to declare given command execution is complete.  
        /// </summary>
        [Button]
        internal void ActionComplete()
        {
            _isBusy = false;
            farmerAction = FarmerActions.Idle;
            _firstDestination = null;
            _endDestination = null;
            SwitchState(walkingState);
        }
        
        internal IEnumerator GetToPositionAndRotation(Vector3 pos, Quaternion rot, float time, Action<bool> callBack)
        {
            float t = 0;
            var position = transform.position;
            var rotation = transform.rotation;
            while (t < time)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(position, pos, t / time);
                transform.rotation = Quaternion.Lerp(rotation, rot, t / time);
                yield return null;
            }
            callBack(true);
            yield break;
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterManager : CharacterManager
{
    [HideInInspector] public AICharacterCombatManager aICharacterCombatManager;
    [HideInInspector] public AIMovementManager aIMovementManager;
    [HideInInspector] public AIAnimatorManager aIAnimatorManager;

    [Header("Navmesh Agent")]
    public NavMeshAgent navMeshAgent;

    [Header("Current State")]
    [SerializeField]AIState currentState;

    [Header("States")]
    public IdleState idle;
    public PursueTargetState pursueTarget;
    public CombatStanceState combatStance;
    public AttackState attack;
    public StunState stun;

    [Header("AI Character Flags")]
    public bool isMoving;
    public bool isStunned = false;

    protected override void Awake()
    {
        base.Awake();

        aICharacterCombatManager = GetComponent<AICharacterCombatManager>();
        aIMovementManager = GetComponent<AIMovementManager>();
        aIAnimatorManager = GetComponent<AIAnimatorManager>();

        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        //Make a copy of this to avoid modifying scriptable object
        idle = Instantiate(idle);
        pursueTarget = Instantiate(pursueTarget);

        currentState = idle;
    }

    protected override void Update()
    {
        base.Update();

        aICharacterCombatManager.HandleActionRecovery(this);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        ProcessStateMachine();
    }

    private void ProcessStateMachine()
    {
        AIState nextState = null;

        if(currentState != null)
        {
            nextState = currentState.Tick(this);
        }

        if(nextState != null)
        {
            currentState = nextState;
        }

        //navMeshAgent.transform.localPosition = Vector3.zero;
        //navMeshAgent.transform.localRotation = Quaternion.identity;

        if(aICharacterCombatManager.CurrentTarget != null)
        {
            aICharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aICharacterCombatManager.CurrentTarget.transform.position);
        }

        if (navMeshAgent.enabled)
        {
            /*
            Vector3 agentDestination = navMeshAgent.destination;
            float remainingDestination = Vector3.Distance(agentDestination, transform.position);

            if(remainingDestination > navMeshAgent.stoppingDistance)
            {
                isMoving = true;
                aIAnimatorManager.PlayMovementAnimator(isMoving);
                //Update movement manually instead of based on animation
            }
            else
            {
                isMoving = false;
                aIAnimatorManager.PlayMovementAnimator(isMoving);
            }
        }
        else
        {
            isMoving = false;
            aIAnimatorManager.PlayMovementAnimator(isMoving);
        }*/

            // Use velocity.magnitude to check if the agent is actually moving
            isMoving = navMeshAgent.velocity.magnitude > 0.1f;
            aIAnimatorManager.PlayMovementAnimator(isMoving);
        }

        //if the player managed to parry, check if this ai is stunned
        //If true, then play stunned animation
    }
}

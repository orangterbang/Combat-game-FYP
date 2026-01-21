using Unity.VisualScripting;
using UnityEngine;

public class AICharacterCombatManager : CharacterCombatManager
{
    [Header("Action Recovery")]
    public float actionRecoveryTimer = 0;

    [Header("Detection")]
    [SerializeField] float detectionRadius = 15;
    [SerializeField] float minimumDetectionAngle = -90;
    [SerializeField] float maximumDetectionAngle = 90;

    public float distanceFromTarget;

    private BoxCollider attackBoxCollider;

    protected override void Awake()
    {
        base.Awake();
        
        attackBoxCollider = attackPoint.GetComponent<BoxCollider>();
        attackBoxCollider.enabled = false;
    }

    public void FindATargetViaLineOfSight(AICharacterManager aiCharacter)
    {
        if(CurrentTarget != null)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.Instance.GetCharacterLayers());

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

            if(targetCharacter == null)
                continue;

            if(targetCharacter == aiCharacter)
                continue;

            //if target character isDead continue

            //Check can i attack this target
            if(WorldUtilityManager.Instance.CanIDamageThisTarget(character.characterGroup, targetCharacter.characterGroup))
            {
                //if potential target is found it has to be infront of the ai
                Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                {
                    //Check for any construct in the way, need a LockOnTransform function to do it(do it later)

                    aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                }
            }
        }
    }

    public void RotateTowardsAgent(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isMoving)
        {
            aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
        }
    }

    public void HandleActionRecovery(AICharacterManager aiCharacter)
    {
        if(actionRecoveryTimer > 0)
        {
            if (!aiCharacter.isPerformingAction)
            {
                actionRecoveryTimer -= Time.deltaTime;
            }
        }
    }

    public void EnableAttackTrigger()
    {
        attackBoxCollider.enabled = true;
        attackBoxCollider.size = attackSize;
    }

    public void DisableAttackTrigger()
    {
        attackBoxCollider.enabled = false;
    }
}

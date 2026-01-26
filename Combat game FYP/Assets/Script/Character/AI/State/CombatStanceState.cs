using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName ="A.I/State/Combat Stance")]
public class CombatStanceState : AIState
{
    //Select an attack for the attack state, depending on distance and direction of target in relation to character
    //Process any combat logic here while waiting to attack
    //If target moves out of combat go to pursue state
    //If target no longer present switch to idle state

    [Header("Attacks")]
    public List<AICharacterAttackAction> aICharacterAttacks; //A list of all attacks this character has
    private List<AICharacterAttackAction> potentialAttacks; //A list of all attacks that the character can do in this or a situation(distance, angle)
    private AICharacterAttackAction choosenAttack;
    private AICharacterAttackAction previousAttack;
    protected bool hasAttack = false;

    [Header("Combo")]//Variables for if the AI can do combo and its chances on doing it
    [SerializeField] protected bool canPerformCombo = false;
    [SerializeField] protected int chanceToPerformCombo = 25;
    protected bool hasRolledForComboChance = false;

    [Header("Engagement Distance")]
    [SerializeField] public float maximumEngagementDistance = 5;

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if (aiCharacter.isPerformingAction)
        {
            return this;
        }

        if (aiCharacter.isStunned)
        {
            return SwitchState(aiCharacter, aiCharacter.stun);
        }

        if (!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        if (aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.animator.applyRootMotion = false;
            
            aiCharacter.navMeshAgent.isStopped = true;
        }

        aiCharacter.aICharacterCombatManager.RotateTowardsAgent(aiCharacter);

        if(aiCharacter.aICharacterCombatManager.CurrentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        if (!hasAttack)
        {
            GetNewAttack(aiCharacter);
        }
        else
        {
            //Check recovery timer
            aiCharacter.attack.currentAttack = choosenAttack;
            //Roll for combo chance
            return SwitchState(aiCharacter, aiCharacter.attack);
        }        

        if(aiCharacter.aICharacterCombatManager.distanceFromTarget > maximumEngagementDistance)// || aiCharacter.aICharacterCombatManager.distanceFromTarget > aiCharacter.navMeshAgent.stoppingDistance
        {
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }

        //NavMeshPath path = new NavMeshPath();
        //aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aICharacterCombatManager.CurrentTarget.transform.position, path);
        //aiCharacter.navMeshAgent.SetPath(path);
        if (aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.isStopped = true; // Stop moving while in stance
        }

        return this;
    }

    protected virtual void GetNewAttack(AICharacterManager aiCharacter)
    {
        potentialAttacks = new List<AICharacterAttackAction>();

        foreach (var potentialAttack in aICharacterAttacks)
        {
            //If too close for this attack, check next
            if(potentialAttack.minimumAttackDistance > aiCharacter.aICharacterCombatManager.distanceFromTarget)
            {
                continue;
            }
            //If too far for this attack, check next
            if(potentialAttack.maximumAttackDistance < aiCharacter.aICharacterCombatManager.distanceFromTarget)
            {
                continue;
            }

            if(potentialAttack.minimumAttackAngle > aiCharacter.aICharacterCombatManager.distanceFromTarget)
            {
                continue;
            }

            if(potentialAttack.maximumAttackDistance < aiCharacter.aICharacterCombatManager.distanceFromTarget)
            {
                continue;
            }

            potentialAttacks.Add(potentialAttack);
        }

        if(potentialAttacks.Count <= 0)
        {
            return;
        }

        var totalWeight = 0;

        foreach (var attack in potentialAttacks)
        {
            totalWeight += attack.attackWeight;
        }

        var randomWeightValue = Random.Range(1, totalWeight + 1);
        var processedWeight = 0;

        foreach (var attack in potentialAttacks)
        {
            processedWeight += attack.attackWeight;

            if(randomWeightValue <= processedWeight)
            {
                choosenAttack = attack;
                previousAttack = choosenAttack;
                hasAttack = true;
                return;
            }
        }
    }

    protected virtual bool RollForOutcomeChance(int outcomeChance)
    {
        bool outcomeWillBePerformed = false;

        int randomPercentage = Random.Range(0, 100);

        if(outcomeChance > randomPercentage)
        {
            outcomeWillBePerformed = true;
        }

        return outcomeWillBePerformed;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        hasRolledForComboChance = false;
        hasAttack = false;
    }
}

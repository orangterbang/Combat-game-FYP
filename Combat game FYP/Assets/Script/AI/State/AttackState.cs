using UnityEngine;

[CreateAssetMenu(menuName ="A.I/State/Attack")]
public class AttackState : AIState
{
    [Header("Current Attack")]
    [HideInInspector] public AICharacterAttackAction currentAttack;
    [HideInInspector] public bool willPerformCombo = false;

    [Header("State Flags")] 
    protected bool hasPerformedAttack = false;
    protected bool hasPerformedCombo = false;

    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if(aiCharacter.aICharacterCombatManager.CurrentTarget == null)
        {
            return SwitchState(aiCharacter, aiCharacter.idle);
        }

        if (aiCharacter.isStunned)
        {
            return SwitchState(aiCharacter, aiCharacter.stun);
        }

        //If the target character is dead, then switch state to idle

        //Rotate towards the target while attacking (Optional/not nessecary)

        //Set Movement value to 0

        //Perform a combo
        if(willPerformCombo && !hasPerformedCombo)
        {
            if(currentAttack.comboAction != null)
            {
                //hasPerformedAttack = true;
                //currentAttack.comboAction.AttemptToPerformAction(aiCharacter);
            }
        }

        if (!hasPerformedAttack)
        {
            if(aiCharacter.aICharacterCombatManager.actionRecoveryTimer > 0)
            {
                return this;
            }

            if (aiCharacter.isPerformingAction)
            {
                aiCharacter.animator.applyRootMotion = true;
                aiCharacter.navMeshAgent.isStopped = true;
                
                return this;
            }

            PerformAttack(aiCharacter);

            return this;
        }

        return SwitchState(aiCharacter, aiCharacter.combatStance);
    }

    protected void PerformAttack(AICharacterManager aiCharacter)
    {
        hasPerformedAttack = true;
        currentAttack.AttemptToPerformAction(aiCharacter);
        aiCharacter.aICharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        hasPerformedAttack = false;
        hasPerformedCombo = false;
    }
}

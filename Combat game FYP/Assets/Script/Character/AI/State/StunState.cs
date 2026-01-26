using UnityEngine;

[CreateAssetMenu(menuName ="A.I/State/Stun")]
public class StunState : AIState
{
    public float stunDuration = 5f;
    private bool hasBeenStunned = false;
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        //1. Have a timer for the stun period
        //2. If timer is 0, switch to combatstancestate else this
        if(aiCharacter.aICharacterCombatManager.stunRecoveryTimer <= 0 && hasBeenStunned)
        {
            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }

        if (!hasBeenStunned)
        {
            SetStun(aiCharacter);
        }
        

        return this;
    }

    protected void SetStun(AICharacterManager aICharacter)
    {
        aICharacter.aICharacterCombatManager.stunRecoveryTimer = stunDuration;
        hasBeenStunned = true;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        aICharacter.isStunned = false;
        hasBeenStunned = false;
    }
}

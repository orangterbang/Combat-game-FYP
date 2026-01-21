using UnityEngine;

[CreateAssetMenu(menuName ="A.I/State/Idle")]
public class IdleState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        if(aiCharacter.characterCombatManager.CurrentTarget != null)
        {
            //Return pursue target state
            return SwitchState(aiCharacter, aiCharacter.pursueTarget);
        }
        else
        {
            //Return this state to find target
            aiCharacter.aICharacterCombatManager.FindATargetViaLineOfSight(aiCharacter);
            
            return this;
        }
    }
}

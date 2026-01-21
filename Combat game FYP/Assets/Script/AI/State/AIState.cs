using UnityEngine;

public class AIState : ScriptableObject
{
    public virtual AIState Tick(AICharacterManager aiCharacter)
    {
        return this;
    }

    protected virtual AIState SwitchState(AICharacterManager aiCharacter, AIState newState)
    {
        ResetStateFlags(aiCharacter);
        return newState;
    }

    protected virtual void ResetStateFlags(AICharacterManager aICharacter)
    {
        //Reset any state flags here so when you return to the state, they are blank once again
        aICharacter.isPerformingAction = false;
        aICharacter.canMove = true;
        aICharacter.canRotate = true;
    }
}

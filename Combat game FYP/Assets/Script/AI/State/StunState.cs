using UnityEngine;

[CreateAssetMenu(menuName ="A.I/State/Stun")]
public class StunState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        //1. Have a timer for the stun period
        //2. If timer is 0, switch to combatstancestate else this
        return this;
    }

    protected override void ResetStateFlags(AICharacterManager aICharacter)
    {
        base.ResetStateFlags(aICharacter);

        aICharacter.isStunned = false;
    }
}

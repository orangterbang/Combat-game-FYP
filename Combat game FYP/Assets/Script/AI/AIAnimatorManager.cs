using UnityEngine;

public class AIAnimatorManager : CharacterAnimatorManager
{
    int stunLayer = 2;

    public void PlayStunnedAnimation()
    {
        // Brief disable to kill base-layer momentum (interrupt)
        character.animator.applyRootMotion = false;

        character.animator.SetLayerWeight(stunLayer, 1f);
        character.animator.SetBool("isStunned", true);

        // Hard play from start (no blend delay)
        character.animator.Play("Stun", stunLayer, 0f);
        character.animator.Update(0f);

        // Re-enable root motion immediately after interrupt
        character.animator.applyRootMotion = true;
    }

    public void StopStunnedAnimation()
    {
        character.animator.SetBool("isStunned", false);
        character.animator.SetLayerWeight(2, 0f);
        character.animator.applyRootMotion = true;
    }
}

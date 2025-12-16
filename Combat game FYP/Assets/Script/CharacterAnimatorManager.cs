using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    //function that do the animation
    public void PlayMovementAnimator(bool isMoving = false)
    {
        character.animator.SetBool("isRunning", isMoving);
    }

    public virtual void PlayTargetActionAnimation(
        string targetAnimation, 
        bool isPerformingAction, 
        int targetLayer = 1,
        bool applyRootMotion = true, 
        bool canRotate = false,
        bool canMove = false)
    {
        character.animator.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f, targetLayer);

        character.isPerformingAction = isPerformingAction;
        character.canMove = canMove;
        character.canRotate = canRotate;
    }
}

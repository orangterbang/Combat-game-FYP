using UnityEngine;

public class AIMovementManager : CharacterMovementManager
{
    public void RotateTowardsAgent(AICharacterManager aICharacter)
    {
        /*if (aICharacter.isMoving)
        {
            aICharacter.transform.rotation = aICharacter.navMeshAgent.transform.rotation;
        }*/

        // Only rotate if the agent is moving
        if (aICharacter.navMeshAgent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aICharacter.navMeshAgent.velocity.normalized);
            aICharacter.transform.rotation = Quaternion.Slerp(aICharacter.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}

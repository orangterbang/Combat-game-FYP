using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

[CreateAssetMenu(menuName ="A.I/State/Pursue Target")]
public class PursueTargetState : AIState
{
    public override AIState Tick(AICharacterManager aiCharacter)
    {
        //Check if we are performing an action, if true wait for the action
        if (aiCharacter.isPerformingAction)
        {
            aiCharacter.navMeshAgent.isStopped = true;
            return this;
        }
        //Check if our target is null, if null then no target, go back to idle state
        if(aiCharacter.aICharacterCombatManager.CurrentTarget == null)
        {
            SwitchState(aiCharacter, aiCharacter.idle);
        }
        //Make sure our nav mesh agent is active, if not enable it
        if (!aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.navMeshAgent.enabled = true;
        }

        if (aiCharacter.navMeshAgent.enabled)
        {
            aiCharacter.animator.applyRootMotion = false;
            
            aiCharacter.navMeshAgent.isStopped = false;
            aiCharacter.navMeshAgent.SetDestination(aiCharacter.aICharacterCombatManager.CurrentTarget.transform.position);
        }

        //aiCharacter.aIMovementManager.RotateTowardsAgent(aiCharacter);
        

        //If we are within combat range , switch state to combat stance/perform attack
        if(aiCharacter.aICharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
        {
            return SwitchState(aiCharacter, aiCharacter.combatStance);
        }
        
        //If target is not reachable and they are far away, return home
        
        //Pursue the target
        //NavMeshPath path = new NavMeshPath();
        //aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aICharacterCombatManager.CurrentTarget.transform.position, path);
        //aiCharacter.navMeshAgent.SetPath(path);

        return this;
    }
}

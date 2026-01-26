using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="A.I/Action/Attack")]
public class AICharacterAttackAction : ScriptableObject
{
    [Header("Attack")]
    [SerializeField] private string attackAnimation;
    [SerializeField] private Vector3 attackSize;

    [Header("Combo Action")]
    public AICharacterAttackAction comboAction; //The combo action of this attack action

    [Header("Action Values")]
    [SerializeField] AttackType attackType;
    public int attackWeight = 50;
    //Attack Type
    //Attack can be repeated?
    public float actionRecoveryTime = 1.5f;
    public float minimumAttackAngle = -35;
    public float maximumAttackAngle = 35;
    public float minimumAttackDistance = 0;
    public float maximumAttackDistance = 2;

    public void AttemptToPerformAction(AICharacterManager aICharacter)
    {
        aICharacter.aICharacterCombatManager.attackSize = attackSize;
        aICharacter.aIAnimatorManager.PlayTargetActionAnimation(attackAnimation, true);
    }
}

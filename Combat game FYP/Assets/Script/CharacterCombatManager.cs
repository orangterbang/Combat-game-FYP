using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterCombatManager : MonoBehaviour
{
    protected CharacterManager character;

    [Header("Target Information")]
    [SerializeField]private CharacterManager currentTarget;

    [Header("Melee Attack Setting")]
    public Transform attackPoint;
    public Vector3 attackSize;
    [SerializeField] protected LayerMask opponentLayerMask = -1;
    [SerializeField] protected float attackDamage = 50f;

    
    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public CharacterManager CurrentTarget
    {
        get => currentTarget;
        set
        {
            if(value == this)
            {
                value = null;
            }

            if(value != null && (!value.gameObject.activeInHierarchy))//add an isDead condition when healt and damage system is added
            {
                value = null;
            }

            currentTarget = value;
        }
    }

    public bool HasTarget => CurrentTarget != null;

    public void ClearTarget()
    {
        CurrentTarget = null;
    }

    public bool IsTargeting(CharacterManager potentialTarget)
    {
        return CurrentTarget == potentialTarget;
    }

    public void SetTarget(CharacterManager potentialTarget)
    {
        CurrentTarget = potentialTarget;
    }

    public virtual void AttackHitCheck()
    {
        
    }
}

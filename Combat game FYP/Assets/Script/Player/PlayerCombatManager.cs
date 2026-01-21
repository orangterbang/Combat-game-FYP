using System;
using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;

    [Header("Parry Setting")]
    private bool isParrying;
    public LayerMask opponentAttackLayerMask = -1;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllCombatAction()
    {
        HandleAttack();
        HandleParry();
    }

    private void HandleAttack()
    {
        if (player.isPerformingAction)
        {
            return;
        }

        if (PlayerInput.Instance.attackInput)
        {
            player.playerAnimatorManager.PlayTargetActionAnimation("Attack", true, 1, false);
        }
    }

    public override void AttackHitCheck()
    {
        base.AttackHitCheck();

        Collider[] hits = Physics.OverlapBox(
            attackPoint.position,
            attackSize,
            attackPoint.rotation,
            opponentLayerMask);

        foreach (var hit in hits)
        {
            if(hit.TryGetComponent(out CharacterManager target))
            {
                if (target == character)
                {
                    continue;
                }

                Debug.Log($"Hit {target.name} for {attackDamage} damage!");
            }
        }
    }

    private void HandleParry()
    {
        if (player.isPerformingAction)
        {
            return;
        }

        if (PlayerInput.Instance.parryInput)
        {
            player.playerAnimatorManager.PlayTargetActionAnimation("Parry", true, 1, false);
        }
    }

    public void EnableParry()
    {
        isParrying = true;
    }

    public void DisableParry()
    {
        isParrying = false;
    }

    public void OnEnemyAttackTrigger(Collider other)
    {
        if(other.gameObject.layer == opponentAttackLayerMask || other.gameObject.layer == LayerMask.NameToLayer("Attack Collider"))
        {
            if (isParrying)
            {
                Debug.Log($"Player parry {other.name}");
                other.GetComponentInParent<AICharacterManager>().isStunned = true;
            }
            else
            {
                Debug.Log($"Player hit by {other.name}");
            }
            
        }
    }
}

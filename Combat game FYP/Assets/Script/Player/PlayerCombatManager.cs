using UnityEngine;

public class PlayerCombatManager : CharacterCombatManager
{
    PlayerManager player;

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
}

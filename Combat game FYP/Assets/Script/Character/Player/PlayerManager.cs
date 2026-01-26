using UnityEngine;

public class PlayerManager : CharacterManager
{
    [HideInInspector]public PlayerMovementManager playerMovementManager;
    [HideInInspector]public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector]public PlayerCombatManager playerCombatManager;
    protected override void Awake()
    {
        base.Awake();

        playerMovementManager = GetComponent<PlayerMovementManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerCombatManager = GetComponent<PlayerCombatManager>();

        PlayerCamera.Instance.player = this;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        
    }
    
    protected override void Update()
    {
        base.Update();
        
        playerMovementManager.HandleAllMovement();
        playerCombatManager.HandleAllCombatAction();
        PlayerCamera.Instance.HandleAllCameraActions();
    }
}

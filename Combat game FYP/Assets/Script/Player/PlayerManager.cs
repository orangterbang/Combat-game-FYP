using UnityEngine;

public class PlayerManager : CharacterManager
{
    PlayerMovementManager playerMovementManager;
    protected override void Awake()
    {
        base.Awake();

        playerMovementManager = GetComponent<PlayerMovementManager>();
        PlayerCamera.Instance.player = this;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        
    }
    
    void Update()
    {
        playerMovementManager.HandleAllMovement();
        PlayerCamera.Instance.HandleAllCameraActions();
    }
}

using UnityEngine;

public class PlayerHurtboxTrigger : MonoBehaviour
{
    [SerializeField] private PlayerCombatManager playerCombatManager;

    void Awake()
    {
        playerCombatManager = GetComponentInParent<PlayerCombatManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(playerCombatManager != null)
        {
            playerCombatManager.OnEnemyAttackTrigger(other);
        }
        else
        {
            Debug.Log("PlayerCombatManager is null");
        }
        
    }
}

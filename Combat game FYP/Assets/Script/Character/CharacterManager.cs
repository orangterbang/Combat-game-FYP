using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector]public CharacterController characterController;
    [HideInInspector]public CharacterCombatManager characterCombatManager;
    [HideInInspector]public Animator animator;
    
    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool canMove = true;
    public bool canRotate = true;
    public bool isInvisible = false;

    [Header("CharacterGroup")]
    public CharacterGroupType characterGroup;

    [Header("Stats")]
    [SerializeField] private CharacterStats template;
    [HideInInspector] public CharacterStats characterStats;


    

    protected virtual void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        characterController = GetComponent<CharacterController>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        animator = GetComponent<Animator>();

        characterStats = Instantiate(template);

        OnSpawn();
    }

    protected virtual void Update()
    {
        if (characterStats.isDead)
        {
            Debug.Log(this.name + " has died!");
            Destroy(gameObject);
        }
    }

    protected virtual void LateUpdate()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnSpawn()
    {
        characterStats.ResetHealth();
        characterStats.ResetFlags();
    }
}

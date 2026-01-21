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

    [Header("CharacterGroup")]
    public CharacterGroupType characterGroup;

    

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        characterController = GetComponent<CharacterController>();
        characterCombatManager = GetComponent<CharacterCombatManager>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void LateUpdate()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }
}

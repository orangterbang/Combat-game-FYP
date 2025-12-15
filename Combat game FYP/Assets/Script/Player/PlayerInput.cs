using JetBrains.Annotations;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    [Header("Locomotion Input")]
    public float MovementInputValue{get; private set;}
    public bool RunInput{get; private set;}
    public bool jumpInput{get; private set;}
    public bool dodgeInput{get; private set;}

    [Header("Combat Input")]
    public bool parryInput{get; private set;}
    public bool attackInput{get; private set;}

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
           Instance = this; 
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        HandleAllInput();
    }

    private void HandleAllInput()
    {
        HandleMovementInput();
        HandleRunInput();
        HandleAttackInput();
        HandleDodgeInput();
        HandleJumpInput();
        HandleParryInput();
    }

    private void HandleMovementInput()
    {
        MovementInputValue = Input.GetAxis("Horizontal");
    }
    private void HandleRunInput()
    {
        RunInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    private void HandleJumpInput()
    {
        jumpInput = Input.GetKeyDown(KeyCode.Space);
    }

    private void HandleParryInput()
    {
        parryInput = Input.GetKeyDown(KeyCode.A);
    }

    private void HandleAttackInput()
    {
        attackInput = Input.GetKeyDown(KeyCode.D);
    }

    private void HandleDodgeInput()
    {
        dodgeInput = Input.GetKeyDown(KeyCode.LeftShift);
    }
}

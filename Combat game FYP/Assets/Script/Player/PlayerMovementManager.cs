using UnityEngine;

//Goal
//1. Make player move left and right
//2. 

public class PlayerMovementManager : CharacterMovementManager
{
    PlayerManager player;

    Vector3 moveDirection;
    Vector3 lastMoveDirection;
    Quaternion targetRotation;

    [SerializeField]float moveSpeed = 3f;
    [SerializeField]float rotationSpeed = 3f;
    [SerializeField]float gravityValue = -30.81f;
    [SerializeField]float verticalVelocity = 0f;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }
    
    void Start()
    {
        lastMoveDirection = Vector3.right;
    }

    public void HandleAllMovement()
    {
        //Ground movement(i.e left-right, dodge)
        //Aerial movement(i.e falling)
        HandleGroundMovement();
    }

    private void HandleGroundMovement()
    {
        //need to check if player on ground
        CalculateVelocity();
        player.characterController.Move(GetMovementVector() * Time.deltaTime);
        MoveDirectionRotate();
    }

    private void CalculateVelocity()
    {
        float horizontalMovement = PlayerInput.Instance.MovementInputValue;
        moveDirection = new Vector3(horizontalMovement, 0, 0);

        if(player.characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravityValue * Time.deltaTime;
        }
    }

    private Vector3 GetMovementVector()
    {
        Vector3 horizontalVelocity = moveDirection * moveSpeed;

        return horizontalVelocity + new Vector3(0f, verticalVelocity, 0f);
    }

    private void MoveDirectionRotate()
    {
        
        if(moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection.normalized;
        }

        if(lastMoveDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(lastMoveDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
    }

    private void HandleDodgeMovement()
    {
        
    }
}

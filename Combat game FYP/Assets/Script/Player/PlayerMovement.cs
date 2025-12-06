using UnityEngine;

public enum MovementState
{
    Normal,
    Dodge
}

public class PlayerMovement : MonoBehaviour
{
    public MovementState state{get; private set;}
    private Attack playerAttack;
    private Parry playerParry;
    private Dodge playerDodge;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack = gameObject.GetComponent<Attack>();
        playerParry = gameObject.GetComponent<Parry>();
        playerDodge = gameObject.GetComponent<Dodge>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerAttack.ExecuteAction();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerParry.ExecuteAction();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float dodgeDirection = Input.GetAxis("Horizontal");

            if(dodgeDirection == 0)
            {
                dodgeDirection = -1f;
            }
            
            //player only teleports for now due to this if statement works in one frame only. the action need to be updated each frame to work smoothly
            playerDodge.moveDirection = new Vector3(dodgeDirection, 0).normalized;
            playerDodge.ExecuteAction();

            playerDodge.dodgeSpeed = 300f;
        }
    }
}

using UnityEngine;

public class Jump : MonoBehaviour, IAction
{
    public float jumpVelocity;
    [SerializeField]private float gravity;
    [SerializeField]private float fallMultiplier;
    public Vector3 jumpPosition;

    private Rigidbody rb;
    private Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ExecuteAction()
    {
        PlayAnim();
        //transform.position += jumpPosition * jumpVelocity * Time.deltaTime;
        rb.linearVelocity = new Vector3(jumpPosition.x * jumpVelocity, jumpVelocity, jumpPosition.z);

        if(jumpVelocity > 0)
        {
            jumpVelocity += -gravity * Time.deltaTime;
        }
        else
        {
            jumpVelocity += -gravity * fallMultiplier * Time.deltaTime;
        }
        
    }

    private void PlayAnim()
    {
        animator.SetTrigger("Jump");
    }
}

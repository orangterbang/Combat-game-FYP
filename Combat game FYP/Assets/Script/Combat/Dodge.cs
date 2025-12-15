using UnityEngine;

public class Dodge : MonoBehaviour, IAction
{
    public float dodgeSpeed;
    private Rigidbody rb;
    public Vector3 moveDirection{ private get; set;}

    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ExecuteAction()
    {
        /*transform.position += moveDirection * dodgeSpeed * Time.deltaTime;

        dodgeSpeed -= dodgeSpeed * 10f * Time.deltaTime;*/
        PlayAnim();

        rb.linearVelocity = moveDirection * dodgeSpeed;

        dodgeSpeed = Mathf.Lerp(dodgeSpeed, 0, 10f * Time.deltaTime);
    }

    private void PlayAnim()
    {
        animator.SetTrigger("Front Roll");
    }
}

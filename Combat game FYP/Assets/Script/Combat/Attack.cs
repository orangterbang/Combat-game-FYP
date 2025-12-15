using UnityEngine;

public class Attack : MonoBehaviour, IAction
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ExecuteAction()
    {
        animator.SetTrigger("Attack");
        Debug.Log("Attack");
    }
}

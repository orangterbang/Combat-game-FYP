using UnityEngine;

public class Parry : MonoBehaviour, IAction
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ExecuteAction()
    {
        animator.SetTrigger("Parry");
        Debug.Log("Parry");
    }
}

using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector]public CharacterController characterController;
    [HideInInspector]public Animator animator;
    
    [Header("Flags")]
    public bool isPerformingAction = false;
    public bool canMove = true;
    public bool canRotate = true;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected virtual void LateUpdate()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

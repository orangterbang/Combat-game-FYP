using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController characterController;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

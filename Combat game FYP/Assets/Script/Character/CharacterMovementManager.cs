using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    protected virtual void Awake()
    {
        
    }

    protected virtual void LateUpdate()
    {
        Vector3 position = transform.position;

        if(position.z != 0)
        {
            position.z = 0;
            transform.position = position;
        }
    }
}

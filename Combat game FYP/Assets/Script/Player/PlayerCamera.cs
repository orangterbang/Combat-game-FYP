using UnityEngine;

//Class to have combat FOV, camera shake etc 

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

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
}

using Unity.VisualScripting;
using UnityEngine;

//Class to have combat FOV, camera shake etc 

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;
    public PlayerManager player;
    public Camera cameraObject;

    [Header("Camera Setting")]
    [SerializeField]private Vector3 cameraVelocity;
    [SerializeField]private Vector3 cameraOffsetPosition;
    [SerializeField]private Vector3 cameraLockPosition;
    [SerializeField]private float cameraSmoothSpeed;

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

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        cameraLockPosition = new Vector3(1,0,1);
    }

    public void HandleAllCameraActions()
    {
        FollowTarget();
        //Camera shake
        //Collide with the edge/something to signify player cannot go there
    }

    private void FollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position + cameraOffsetPosition, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = Vector3.Scale(targetCameraPosition, cameraLockPosition);
    }
}

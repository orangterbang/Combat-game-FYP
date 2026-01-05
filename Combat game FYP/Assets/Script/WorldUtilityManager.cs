using Unity.VisualScripting;
using UnityEngine;

public class WorldUtilityManager : MonoBehaviour
{
    public static WorldUtilityManager Instance;

    public LayerMask environmentLayers;
    public LayerMask characterLayers;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public LayerMask GetEnviromentLayer()
    {
        return environmentLayers;
    }

    public LayerMask GetCharacterLayers()
    {
        return characterLayers;
    }
}

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

    public bool CanIDamageThisTarget(CharacterGroupType attackingCharacter, CharacterGroupType targetCharacter)
    {
        if(attackingCharacter == CharacterGroupType.Team1)
        {
            switch (targetCharacter)
            {
                case CharacterGroupType.Team1: return false;
                case CharacterGroupType.Team2: return true;
                default: 
                    break;
            }
        }else if(attackingCharacter == CharacterGroupType.Team2)
        {
            switch (targetCharacter)
            {
                case CharacterGroupType.Team1: return true;
                case CharacterGroupType.Team2: return false;
                default: 
                    break;
            }
        }

        return false;
    }
}

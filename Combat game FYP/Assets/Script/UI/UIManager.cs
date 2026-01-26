using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private CharacterManager character;
    public GameObject characterGameObject;
    public TMP_Text healthText;
    private int currentHealth;
    private int totalHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(healthText == null)
        {
            healthText = GetComponent<TMP_Text>();
        }

        character = characterGameObject.GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (int)character.characterStats.currentHealth;
        totalHealth = (int)character.characterStats.totalHealth;

        healthText.text = currentHealth + "/" + totalHealth;
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}

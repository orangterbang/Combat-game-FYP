using UnityEngine;

[CreateAssetMenu(menuName ="Character/Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")]
    public float totalHealth; 
    public float currentHealth;

    [Header("Flag")]
    public bool isDead;

    public void ResetHealth()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void ResetFlags()
    {
        isDead = false;
    }
}

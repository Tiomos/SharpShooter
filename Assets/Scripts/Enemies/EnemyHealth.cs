using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int weaponDmg)
    {
        currentHealth -= weaponDmg;

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
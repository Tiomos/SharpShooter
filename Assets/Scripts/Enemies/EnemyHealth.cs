using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] int startingHealth = 3;

    int currentHealth;

    GameManager gameManager;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager?.UpdateEnemiesLeft(1);
    }

    public void TakeDamage(int weaponDmg)
    {
        currentHealth -= weaponDmg;

        if (currentHealth <= 0)
        {
            gameManager?.UpdateEnemiesLeft(-1);
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
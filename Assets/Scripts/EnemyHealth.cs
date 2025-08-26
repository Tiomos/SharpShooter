using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    int currentHealth;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int weaponDmg)
    {
        currentHealth -= weaponDmg;
        Debug.Log("Enemy HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Debug.Log("Przeciwnik zniszczony");
        Destroy(this.gameObject);
    }
}
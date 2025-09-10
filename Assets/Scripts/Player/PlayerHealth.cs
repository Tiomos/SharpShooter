using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;
    [SerializeField] CinemachineVirtualCamera deathCam;
    [SerializeField] Transform weaponCamera;

    int currentHealth;
    int deathCamPriority = 20;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player hit! Current health: " + currentHealth + "for " + amount + " damage.");

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathCam.Priority = deathCamPriority;
            Destroy(this.gameObject);
        }
    }
}

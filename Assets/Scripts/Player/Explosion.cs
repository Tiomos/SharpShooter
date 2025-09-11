using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int explosionDmg = 2;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();

            if (playerHealth == null) continue;

            playerHealth.TakeDamage(explosionDmg);

            break;

        }
    }

}

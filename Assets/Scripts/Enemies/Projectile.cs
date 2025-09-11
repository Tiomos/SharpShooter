using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 5f;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] int damage = 2;
    [SerializeField] GameObject destroyEffect;


    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed;
        Destroy(gameObject, lifeTime);
    }

    void OnDestroy()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth?.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}

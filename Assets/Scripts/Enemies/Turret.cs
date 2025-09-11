using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float shootRange = 65f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float rotateSpeed = 1f;

    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 shootPoint = player.transform.position + Vector3.up * 0.3f;
            Vector3 direction = (shootPoint - turretHead.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            turretHead.rotation = Quaternion.Lerp(turretHead.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }  
    }

    IEnumerator Shoot()
    {
        while (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= shootRange)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, turretHead.rotation); 
                yield return new WaitForSeconds(fireRate); 
            }
            else
            {
                yield return null; 
            }
        }
    }
}

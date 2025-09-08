using StarterAssets;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            muzzleFlash.Play();
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
            Quaternion rot = Quaternion.LookRotation(hit.normal);
            Instantiate(weaponSO.HitVFXPrefab, hit.point, rot);
        }
    }
}

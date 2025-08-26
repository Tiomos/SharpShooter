using StarterAssets;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int weaponDmg = 1;
    StarterAssetsInputs starterAssetsInputs;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    void Update()
    {
        HandleShoot();
    }

    private bool HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return false;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponDmg);

            starterAssetsInputs.ShootInput(false);
        }

        return true;
    }
}

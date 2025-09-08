using UnityEngine;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;

    const string SHOOT_STRING = "Shoot";

    float lastShootTime = 0f;

    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        lastShootTime += Time.deltaTime;
        HandleShoot();
    }


    void HandleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        if (lastShootTime >= weaponSO.FireRate)
            {
                currentWeapon.Shoot(weaponSO);
                animator.Play(SHOOT_STRING, 0, 0f);
                lastShootTime = 0f;
            }
            
         starterAssetsInputs.ShootInput(false);
    }
}

using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponFollowCamera;
    [SerializeField] GameObject zoomVinette;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    const string SHOOT_STRING = "Shoot";

    float lastShootTime = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;


    void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    void Start()
    {
        SwitchWeapon(startingWeaponSO);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, currentWeaponSO.MagazineSize);
        ammoText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        if (newWeapon != null)
        {
            currentWeapon = newWeapon;
            this.currentWeaponSO = weaponSO;
            AdjustAmmo(currentWeaponSO.MagazineSize);
        }
    }

    void HandleShoot()
    {
        lastShootTime += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;
        if (lastShootTime >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            lastShootTime = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;


        if (starterAssetsInputs.zoom)
        {
            weaponFollowCamera.fieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, currentWeaponSO.ZoomFOV, Time.deltaTime * 15f);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, currentWeaponSO.ZoomFOV, Time.deltaTime * 15f);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
            zoomVinette.SetActive(true);
        }
        else
        {
            weaponFollowCamera.fieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
            playerFollowCamera.m_Lens.FieldOfView = Mathf.Lerp(playerFollowCamera.m_Lens.FieldOfView, defaultFOV, Time.deltaTime * 10f);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
            zoomVinette.SetActive(false);
        }
    }
}

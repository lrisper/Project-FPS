using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager _weaponManager;

    public float fireRate = 15f;
    private float _nextTimeToFire;
    public float damage = 20f;

    private Animator _zoomCameraAnim;
    private bool _zoomed;
    private Camera _mainCam;

    private GameObject _crosshair;
    private bool _isAiming;

    [SerializeField] private GameObject _arrowPrefab, _spearPrefab;
    [SerializeField] private Transform _arrowBowStartPosition;


    public void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
        _zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        _crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        _mainCam = Camera.main;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        if (_weaponManager.GetCurrentSelectedWeapon().weaponFireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                if (_weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.BULLET)
                {
                    _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    BulletFired();
                }
                else
                {
                    if (_isAiming)
                    {
                        _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (_weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.ARROW)
                        {
                            ThrowArrowOrSpear(true);
                        }
                        else if (_weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }

    void ZoomInAndOut()
    {
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                _crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                _zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                _crosshair.SetActive(true);
            }
        }
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(true);
                _isAiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(false);
                _isAiming = false;
            }
        }
    }
    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(_arrowPrefab);
            arrow.transform.position = _arrowBowStartPosition.position;
            arrow.GetComponent<ArrowBowScript>().Lunch(_mainCam);
        }
        else
        {
            GameObject spear = Instantiate(_spearPrefab);
            spear.transform.position = _arrowBowStartPosition.position;
            spear.GetComponent<ArrowBowScript>().Lunch(_mainCam);
        }
    }
    void BulletFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out hit))
        {
            //
        }
    }
}

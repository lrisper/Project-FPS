using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager _weaponManager;

    public float fireRate = 15f;
    private float _nextTimeToFire;
    public float damage = 20f;

    public void Awake()
    {
        _weaponManager = GetComponent<WeaponManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
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

                }
            }
        }
    }
}

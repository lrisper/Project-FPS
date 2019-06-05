using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponHandler : MonoBehaviour
{

    private Animator _anim
    ;
    public WeaponAim weaponAim;
    [SerializeField] private GameObject _muzzleFlash;

    public AudioSource shotSound, reloadSound;

    public WeaponFireType weaponFireType;
    public WeaponBulletType weaponBulletType;
    public GameObject attackPoint;

    void Awake()
    {
        _anim = GetComponent<Animator>();

    }

    public void ShootAnimation()
    {
        _anim.SetTrigger(AnimationTags.SHOOT_Trigger);
    }
    public void Aim(bool canAim)
    {
        _anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);

    }

    public void TurnOnMuzzleFlash()
    {
        _muzzleFlash.SetActive(true);
    }

    public void TurnOffMuzzleFlash()
    {
        _muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shotSound.Play();
    }

    void PlayReloadSound()
    {
        reloadSound.Play();
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }

    }
}

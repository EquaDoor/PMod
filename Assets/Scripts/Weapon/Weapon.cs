using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public float damage = 5; // урон
    public float fireRate = 0.25f; // скорострельность
    public float rateTimer; // таймер выстрелов
    public float reloadTime = 0.5f; // время перезарядки
    public int currentAmmo; // сейчас патронов
    public int maxAmmo = 30; // макс патронов
    public bool isAuto = true; // автоматическое ли оружие
    public Camera cam; // камера

    [Header("Weapon Effects")]
    public ParticleSystem bulletImpact; // эффект попадания
    public ParticleSystem muzzleFlash; // дульное пламя
    public AudioSource shootSource; // звук выстрела
    public Animator anim; // аниматор оружия


    public virtual void Init(Camera _cam, Animator _anim)
    {
        this.cam = _cam;
        this.anim = _anim;

        currentAmmo = maxAmmo;
    }

    public virtual void Shoot()
    {
        if(currentAmmo <= 0) return; // если нет патронов

        currentAmmo--; // уменьшаем счётчик патронов

        rateTimer = fireRate;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            if(hit.transform.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(damage);
            }

            Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            Debug.DrawLine(cam.transform.position,hit.point, Color.red, 1f);
        }
        muzzleFlash.Play();
    }

    public virtual void Reload()
    {
        // anim.SetTrigger("Reload");
        Invoke(nameof(SetAmmo), reloadTime);
    }

    private void SetAmmo()
    {
        currentAmmo = maxAmmo;
    }

}
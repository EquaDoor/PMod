using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private float damage; // урон
    [SerializeField] private float fireRate; // скорострельность
    [SerializeField] private int currentAmmo; // сейчас патронов
    [SerializeField] private int maxAmmo; // макс патронов
    [SerializeField] private bool isAuto; // автоматическое ли оружие
    private Camera cam; // камера

    [Header("Weapon Effects")]
    [SerializeField] private ParticleSystem bulletImpact; // эффект попадания
    [SerializeField] private ParticleSystem muzzleFlash; // дульное пламя
    [SerializeField] private AudioSource shootSource; // звук выстрела
    private Animator anim; // аниматор оружия


    public virtual void Init(Camera _cam, Animator _anim)
    {
        this.cam = _cam;
        this.anim = _anim;
    }

    public virtual void Shoot()
    {
        if(currentAmmo <= 0) return; // если нет патронов

        currentAmmo--; // уменьшаем счётчик патронов

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            // if(hit.transform.TryGetComponent(out Damageable damageable))
            // {
            //     damageable.TakeDamage(damage);
            // }

            Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
            muzzleFlash.Play();
            Debug.DrawLine(cam.transform.position,hit.point, Color.red, 1f);
        }
    }

    public virtual void Reload()
    {
        anim.SetTrigger("Reload");
    }

}
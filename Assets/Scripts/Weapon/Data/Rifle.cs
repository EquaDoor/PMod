using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    private void Update() {
        if(rateTimer > 0) rateTimer -= Time.deltaTime;

        if(Input.GetKey(KeyCode.Mouse0) && rateTimer <= 0) Shoot();
        if(Input.GetKeyDown(KeyCode.R)) Reload();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void Reload()
    {
        base.Reload();

        Invoke(nameof(SetAmmo),.5f);
    }

    private void SetAmmo()
    {
        currentAmmo = maxAmmo;
    }
}

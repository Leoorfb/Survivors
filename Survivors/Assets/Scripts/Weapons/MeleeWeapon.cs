using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public override void Attack()
    {
        _projectilePool.Get();

        //Debug.Log(name + " attack");
        StartCoroutine("CooldownAttack");
    }

    protected override WeaponProjectile CreateProjectile()
    {
        WeaponProjectile projectile = GameObject.Instantiate(projectilePrefab, transform.position, transform.rotation, transform).GetComponent<WeaponProjectile>();
        projectile.weapon = this;
        projectile.Init(DisableProjectile);
        return projectile;
    }
}

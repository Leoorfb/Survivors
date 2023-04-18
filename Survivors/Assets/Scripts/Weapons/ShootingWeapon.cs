using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : WeaponBase
{
    [SerializeField] GameObject projectilePrefab;


    public override void Attack()
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, transform.position, transform.rotation, projectilesContainer);
        projectile.GetComponent<WeaponProjectile>().weapon = this;

        //Debug.Log(name + " attack");
        StartCoroutine("CooldownAttack");
    }
}

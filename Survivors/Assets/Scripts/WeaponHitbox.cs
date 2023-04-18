using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hitbox colidiu com " + other.gameObject.name);
        if (other.gameObject.tag == "Enemy")
        {
            transform.parent.GetComponent<WeaponProjectile>().weapon.HitEnemy(other.gameObject.GetComponent<EnemyChase>());
        }
    }
}

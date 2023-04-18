using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifespan;
    public WeaponBase weapon;

    private void Start()
    {
        StartCoroutine("AttackDuration");

    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            weapon.HitEnemy(other.gameObject.GetComponent<EnemyChase>());
        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(lifespan);
        GameObject.Destroy(gameObject);
    }
}

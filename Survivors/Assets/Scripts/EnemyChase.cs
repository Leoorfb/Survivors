using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    Transform playerTransform;

    [SerializeField] int hp = 10;
    [SerializeField] float speed = 4;
    [SerializeField] int damage = 1;

    [SerializeField] bool isAttackOnCooldown = false;
    [SerializeField] float attackCooldown = 0.2f;

    [SerializeField] GameObject Drop;

    Vector3 moveDirection = new Vector3(0, 0, 0);
    float step = 0.1f;

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        moveDirection.x = playerTransform.position.x - transform.position.x;
        moveDirection.z = playerTransform.position.z - transform.position.z;
        moveDirection = moveDirection.normalized;
        //Debug.Log(direction);
        step = speed * Time.deltaTime;
        transform.Translate(moveDirection * step);
    }

    public int Attack()
    {
        if (isAttackOnCooldown || !gameObject.activeInHierarchy)
        {
            return 0;
        }

        isAttackOnCooldown = true;
        StartCoroutine("CooldownAttack");
        return damage;
    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttackOnCooldown = false;
    }

    public void TakeDamage(int damage)
    {
        if (damage != 0)
        {
            hp -= damage;
            //Debug.Log(damage + " damage taken");

            if (hp <= 0)
            {
                Die();
            }
            //Debug.Log(name + " hp: " + hp);

        }
        return;
    }

    private void Die()
    {
        GameObject.Instantiate(Drop, transform.position, transform.rotation);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void SetPlayerTransform(Transform nPlayerTransform)
    {
        playerTransform = nPlayerTransform;
    }
}

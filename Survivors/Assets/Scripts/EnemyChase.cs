using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    Transform playerTransform;

    public int hp = 10;
    public int maxHp = 10;
    [SerializeField] float speed = 4;
    [SerializeField] int damage = 1;

    public bool isAttackOnCooldown = false;
    [SerializeField] float attackCooldown = 0.2f;

    Vector3 moveDirection = new Vector3(0, 0, 0);
    float step = 0.1f;

    private Action<EnemyChase> _killAction;


    Rigidbody _Rigidbody;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

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

        //_Rigidbody.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.VelocityChange);

        //transform.rotation = Quaternion.LookRotation(moveDirection);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);

        _Rigidbody.velocity = moveDirection * speed; //* Time.deltaTime;
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

    public void Init(Action<EnemyChase> killAction)
    {
        _killAction = killAction;
    }

    public void TakeDamage(int damage)
    {
        if (damage != 0)
        {
            hp -= damage;
            //Debug.Log(damage + " damage taken");

            if (hp <= 0)
            {
                _killAction(this);
            }
            //Debug.Log(name + " hp: " + hp);

        }
        return;
    }

    public void SetPlayerTransform(Transform nPlayerTransform)
    {
        playerTransform = nPlayerTransform;
    }
}

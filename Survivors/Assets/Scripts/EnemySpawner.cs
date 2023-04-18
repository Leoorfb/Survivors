using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnRadius;
    Transform playerTransform;

    [SerializeField] Transform DropContainer;
    [SerializeField] GameObject dropItem;

    [SerializeField] float spawnCooldown;

    private ObjectPool<EnemyChase> _enemyPool;
    private ObjectPool<Exp> _dropPool;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = new ObjectPool<EnemyChase>(CreateEnemy, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);
        _dropPool = new ObjectPool<Exp>(CreateDrop, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject);

        playerTransform = GameManager.instance.playerTransform;
        StartCoroutine("CooldownSpawn");
    }

    private EnemyChase CreateEnemy()
    {
        EnemyChase newEnemy = GameObject.Instantiate(EnemyPrefab, transform).GetComponent<EnemyChase>();
        newEnemy.SetPlayerTransform(playerTransform);
        newEnemy.Init(KillEnemy);
        return newEnemy;
    }
    void OnReturnedToPool(EnemyChase enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    void OnTakeFromPool(EnemyChase enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    void OnDestroyPoolObject(EnemyChase enemy)
    {
        Destroy(enemy.gameObject);
    }
    
    private Exp CreateDrop()
    {
        Exp newExp = GameObject.Instantiate(dropItem, DropContainer).GetComponent<Exp>();
        newExp.Init(DisableCollectable);
        return newExp;
    }
    void OnReturnedToPool(Exp drop)
    {
        drop.gameObject.SetActive(false);
    }
    void OnTakeFromPool(Exp drop)
    {
        drop.gameObject.SetActive(true);
    }
    void OnDestroyPoolObject(Exp drop)
    {
        Destroy(drop.gameObject);
    }

    void OnSetUpEnemy(EnemyChase enemy)
    {
        Vector3 spawnPosition = GeneratePosition();
        spawnPosition += playerTransform.position;
        enemy.transform.position = spawnPosition;

        enemy.hp = enemy.maxHp;
        enemy.isAttackOnCooldown = false;
    }

    private void SpawnEnemy()
    {
        EnemyChase nEnemy = _enemyPool.Get();
        OnSetUpEnemy(nEnemy);

        StartCoroutine("CooldownSpawn");
    }

    private Vector3 GeneratePosition()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10)).normalized;
        //Debug.Log(position + " mag:" + position.magnitude);

        if (position.magnitude == 0)
        {
            position.x = 1;
        }

        position.x *= spawnRadius;
        position.z *= spawnRadius;
        //Debug.Log(position + " mag:" + position.magnitude);

        return position;
    }

    private IEnumerator CooldownSpawn()
    {
        yield return new WaitForSeconds(spawnCooldown);
        SpawnEnemy();
    }

    private void KillEnemy(EnemyChase enemy)
    {
        Exp drop = _dropPool.Get();
        drop.transform.position = enemy.transform.position;

        enemy.StopAllCoroutines();
        _enemyPool.Release(enemy);
    }

    private void DisableCollectable(Exp exp)
    {
        _dropPool.Release(exp);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnRadius;
    Transform playerTransform;

    [SerializeField] float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
        StartCoroutine("CooldownSpawn");
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GeneratePosition();
        spawnPosition += playerTransform.position;
        //Debug.Log(spawnPosition + " mag:" + spawnPosition.magnitude);


        GameObject newEnemy = GameObject.Instantiate(EnemyPrefab, spawnPosition, transform.rotation, transform);
        newEnemy.GetComponent<EnemyChase>().SetPlayerTransform(playerTransform);
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
}

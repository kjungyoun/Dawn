using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingGameManager : MonoBehaviour
{
    // 적이 스폰되는 것에 대한 로직이다.
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    // 시간을 계속 확인하면서 max 시간이 넘었을 때 몬스터 리젠한다.
    void Update()
    {
        curSpawnDelay += Time.deltaTime; // 시간 계속 더하고

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy(); // 시간이 초과되었을 때 SpawnEnemy을 다시 작동시킨다. 
            maxSpawnDelay = Random.Range(0f, 2f); // 랜덤으로 max를 다시 뽑는다.
            curSpawnDelay = 0;
        }
    }


    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);
        Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);

    }
}

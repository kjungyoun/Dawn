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

    public GameObject player;
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


    // 적기 생성
    void SpawnEnemy()
    {
        // 적 소환에서 양 옆으로 point가 생성되었기 때문에 해당 부분에 대한 코드가 따로 필요함

        // 모든 적에 대해 객체로 받아들임
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 9); // 5개에서 9개로 바꾸고
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        ShootingEnemy enemyLogic = enemy.GetComponent<ShootingEnemy>();
        enemyLogic.player = player;

        // 양 옆으로 나타나는 적에 대한 소환 로직
        if (ranPoint == 5 || ranPoint == 7) // 왼쪽 스폰
        {
            enemy.transform.Rotate(Vector3.forward * 90); // 회전 해줘야함
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if(ranPoint == 6 || ranPoint == 8) // 오른쪽 스폰
        {
            enemy.transform.Rotate(Vector3.back * 90); // 회전 해줘야함
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else // front 스폰
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

        }
    }


    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
    }
}

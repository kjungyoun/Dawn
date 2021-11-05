using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootingGameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImages;
    public GameObject gameOverSet;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > maxSpawnDelay)
        {
            maxSpawnDelay = Random.Range(0f, 2f);
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        //#.UI Score Update
        ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); // 숫자가 3개씩 끊겨져 있는 문자를 숫자로 변환하는 법
     }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);
        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Rigidbody2D Rigid = enemy.GetComponent<Rigidbody2D>();
        ShootingEnemy enemyLogic = enemy.GetComponent<ShootingEnemy>();
        enemyLogic.player = player; // Player 변수화
    }

    // UI image 관리
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 0);
        }

        for (int index=0; index < life; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 1);
        }
    }

    // respawn player
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4f;
        player.SetActive(true);

        ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
        playerLogic.isHit = false;
    }


    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }


    public void RetryGame()
    {
        SceneManager.LoadScene(0);
    }

}

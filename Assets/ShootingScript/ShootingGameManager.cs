using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ShootingGameManager : MonoBehaviour
{
    // stage
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;

    public Transform playerPos;

    // 적이 스폰되는 것에 대한 로직이다.
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;

    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;
    public ShootingObjectManager objectManager;

    public List<ShootingSpawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<ShootingSpawn>();
        enemyObjs = new string[] { "EnemyL", "EnemyM", "EnemyS", "EnemyB" };
        ReadSPawnFile();
    }

    // 리스폰 데이터 생성
    void ReadSPawnFile()
    {
        // 1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 2. spawnFile 읽기
        TextAsset textFile = Resources.Load("stage " + stage) as TextAsset;
        StringReader stringreader = new StringReader(textFile.text);

        while(stringreader != null)
        {
            string line = stringreader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;

            ShootingSpawn spawnData = new ShootingSpawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // 마지막 텍스트 파일은 꼭 닫기!
        stringreader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    public void StageStart()
    {
        // Stage UI load
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart!!";
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear!!";
        // Enemy Spawn File Read
        ReadSPawnFile();

        // Fade In
        fadeAnim.SetTrigger("In");

    }

    public void StageEnd()
    {
        // Clear UI load
        clearAnim.SetTrigger("On");

        // Fade Out
        fadeAnim.SetTrigger("Out");

        // Player RePos
        player.transform.position = playerPos.position;

        // Stage Increment
        stage++;
        if(stage > 2)
        {
            Invoke("GameOver", 2);
        }
        else
        {
            Invoke("StageStart", 5);
        }

    }

    // 시간을 계속 확인하면서 max 시간이 넘었을 때 몬스터 리젠한다.
    void Update()
    {
        curSpawnDelay += Time.deltaTime; // 시간 계속 더하고

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy(); // 시간이 초과되었을 때 SpawnEnemy을 다시 작동시킨다. 
            curSpawnDelay = 0;
        }

        // UI score Update
        ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score); // 0:n0 3자리 마다 쉼표로 나눠주는 숫자 양식

    }
    // 적기 생성
    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }

        // 적 소환에서 양 옆으로 point가 생성되었기 때문에 해당 부분에 대한 코드가 따로 필요함

        // 모든 적에 대해 객체로 받아들임
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
        
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        ShootingEnemy enemyLogic = enemy.GetComponent<ShootingEnemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this; // 나 자신을 의
        enemyLogic.objectManager = objectManager;

        // 양 옆으로 나타나는 적에 대한 소환 로직
        if (enemyPoint == 5 || enemyPoint == 7) // 왼쪽 스폰
        {
            enemy.transform.Rotate(Vector3.forward * 90); // 회전 해줘야함
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if (enemyPoint == 6 || enemyPoint == 8) // 오른쪽 스폰
        {
            enemy.transform.Rotate(Vector3.back * 90); // 회전 해줘야함
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else // front 스폰
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));

        }
        // 리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // 다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;

    }

    public void UpdateLifeIcon(int life)
    {
        // UI init the Disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        // UI life Active
        for (int index=0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        // UI init the Disable
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }

        // UI boom Active
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
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

        ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
        playerLogic.isHit = false;
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        ShootingExplosion explosionLogic = explosion.GetComponent<ShootingExplosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(21); // 21(build setting에 있는 번)을 적어도 되고 실제 그 Scene의 이름을 적어도 해당 씬의 처음으로 넘어가게 됨.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public string enemyName; // 이걸 전달해서 적이 총을 어떻게 쏠지 구분 짓게 해줄것임
    public float speed; // 적 속도
    public int enemyScore; // 적 점수
    public int health; // 적 체력
    public Sprite[] sprites; // 적이 맞았을 때 깜박깜박 거리게 해주는 UI를 담을 배열

    public GameObject bulletObjA; // 총알1 obj
    public GameObject bulletObjB; // 총알2 obj
    public GameObject player; // player 받기

    // 적 생성 및 총발 관리
    public ShootingObjectManager objectManager;
    public ShootingGameManager gameManager;

    // 아이템 드랍
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;

    // 보스 패턴
    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    // 적 총 발사(패턴)
    public float maxShotDelay; // max에 알맞게 왔으면 다시 curShotDelay을 0으로 초기화하면서 쏘고
    public float curShotDelay;

    Animator anim;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Update()
    {
        if(enemyName == "B")
        {
            return;
        }
        Fire(); // 발사
        Reload(); // 장전
    }

    void Fire()
    { 
        if (curShotDelay < maxShotDelay) //  일정 시간 지나면 알아서 총 쏘게끔 유도
            return;

        if(enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 3, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 3, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Awake()
    {
        // sprite 적용 (맞으면 당연히 깜박깜박 걸려야함)
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(enemyName == "B")
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnEnable() // 컴포넌트가 활성화될 때 호출되는 생명주기 함수 OnEnable
    {
        switch(enemyName)
        {
            case "B":
                health = 5000;
                Invoke("Stop", 2f);
                break;
            case "L":
                health = 35;
                Invoke("StopEnemyNotBoos", 3f);
                break;
            case "M":
                health = 10;
                break;
            case "S":
                health = 5;
                Invoke("StopEnemyNotBoos", 3f);
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2f);
    }

    void StopEnemyNotBoos()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
    }

    void Think() // 보스가 공격을 뭐로 할지 생각하는 것임
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    // 보스 공격 함수 4개 만들기
    void FireForward()
    {
        if (health <= 0)
            return;
        Debug.Log("직선 발사");
        // Fire Four Bullet Forward
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.8f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.8f;


        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        // Pattern Counting
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex]){
            Invoke("FireForward", 2f);
        }
        else
        {
            Invoke("Think", 3f);
        }
            
    }

    void FireShot()
    {
        if (health <= 0)
            return;
        for (int index=0; index < 8; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-7f, 7f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 2f);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    void FireArc()
    {
        if (health <= 0)
            return;
        GameObject bullet = objectManager.MakeObj("BulletEnemyA");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 3 * (curPatternCount)/maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    void FireAround()
    {
        if (health <= 0)
            return;
        int roundNumA = 15;
        int roundNumB = 14;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int index=0; index < roundNum; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulleBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum),
                                         Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward*90;
            bullet.transform.Rotate(rotVec);
        }
        

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        if(enemyName == "B")
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.5f);
        }
        else
        {
            spriteRenderer.sprite = sprites[1]; // 적이 내 총알을 맞았을 때, 흰색으로 변함
            Invoke("ReturnSprite", 0.5f);
        }
        
        if (health <= 0)
        {
            ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
            playerLogic.score += enemyScore;

            // Random Ratio Item Drop
            int ran = enemyName == "B" ? 0 : Random.Range(0, 10);
            if( ran <= 1)
            {
                Debug.Log("Not Item");
            }
            else if(ran < 2) // Coin
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if(ran < 6) // boom
            {
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            else if(ran < 10) // power
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            if (enemyName == "B")
            {
                objectManager.DeleteAllObj("Boss");
            }
            gameObject.SetActive(false);
            CancelInvoke();
            transform.rotation = Quaternion.identity; // 기본 회전값 0
            gameManager.CallExplosion(transform.position, enemyName);

            // Boss Kill
            if(enemyName == "B")
            {
                gameManager.StageEnd();
            }
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0]; // 적이 내 총알을 맞았을 때, 다시 돌아옴
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            ShootingBullet bullet = collision.GetComponent<ShootingBullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }
}

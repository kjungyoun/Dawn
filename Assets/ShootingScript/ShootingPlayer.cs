using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour
{
    
    // Border와 만났는지 알아보는 방법 isTouch를 추가한다.
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life; // 목숨
    public int score; // 점수
    public int boom; // 폭탄개수
    public int maxBoom; // 최대 폭탄개수

    // 총알 변수
    public GameObject bulletObjA; // 총알 타입 A
    public GameObject bulletObjB; // 총알 타입 B
    public GameObject boomEffect; // 폭탄 이펙트
    public GameObject[] followers; // followers 만들기

    // player respawn 관련해서 사용할 Manager
    public ShootingGameManager GameManager;
    public ShootingObjectManager objectManager; // 오브젝트 폴 담당

    // 총알 나오는 횟수
    public float speed; // 비행기 속도
    public float maxPower; // 최대파워
    public float power; // 파워
    

    public float maxShotDelay; // max에 알맞게 왔으면 다시 curShotDelay을 0으로 초기화하면서 쏘고
    public float curShotDelay; // 총알 빈도

    public bool isHit; // 한번에 여러번 life가 줄어드는 것을 방지
    public bool isBoomTime; // 현재 폭탄을 쓰고 있는지?
    public bool isRespawnTime;

    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;
    public bool isButtonB;

    // Animator 초기화
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {

        Unbeatable();
        Invoke("Unbeatable", 3);
        
    }

    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime;
        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            for(int index=0; index<followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            for (int index = 0; index < followers.Length; index++)
            {
                followers[index].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    void Update()
    {
        Move(); // 이동
        Fire(); // 발사
        Reload(); // 장전
        Boom(); // 폭탄 터짐
    }

    public void JoyPanel(int type)
    {
        for(int index=0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }

    public void JoyUp()
    {
        isControl = false;
    }

    void Move()
    {
        // Keyboard Control
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Joy Control Value
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }


        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) || !isControl)
            h = 0;

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) || !isControl)
            v = 0;

        Vector3 curPos = transform.position; // 현재 위치
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    public void ButtonADown()
    {
        isButtonA = true;
    }

    public void ButtonAUp()
    {
        isButtonA = false;
    }

    public void ButtonBDown()
    {
        isButtonB = true;
    }

    void Fire()
    {
        //if (!Input.GetButton("Fire1"))
        //    return;

        if (!isButtonA)
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                
                bulletR.transform.position = transform.position + Vector3.right * 0.3f;
                bulletL.transform.position = transform.position + Vector3.left * 0.3f; ;
                
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
            default: // 1과 2를 제외한 나머지 모든 int형에 대해 저용할 수 있음
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerB");
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");

                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                bulletCC.transform.position = transform.position;
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;

                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

                rigidRR.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
        }

        curShotDelay = 0f;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        //if (!Input.GetButton("Fire2"))
        //    return;

        if (!isButtonB)
            return;

        if (isBoomTime)
            return;

        if (boom == 0)
            return;

        boom--;
        isBoomTime = true;
        GameManager.UpdateBoomIcon(boom); // 썼을 때 남은 개수 반영

        // 1. Effect visible
        boomEffect.SetActive(true); // 효과 키기! 키면 당연히 꺼야지?
        Invoke("OffBoomEffect", 2f);

        // 부하줄이기!
        // 2. Remove Enemy
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");

        RemoveEnemy(enemiesL);
        RemoveEnemy(enemiesM);
        RemoveEnemy(enemiesS);

        // 3.Remove Enemy Bullet
        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");

        RemoveEnemyBullet(bulletsA);
        RemoveEnemyBullet(bulletsB);
    }

    void RemoveEnemy(GameObject[] enemy)
    {
        for (int index = 0; index < enemy.Length; index++)
        {
            if (enemy[index].activeSelf)
            {
                ShootingEnemy enemyLogic = enemy[index].GetComponent<ShootingEnemy>();
                enemyLogic.OnHit(1000);
            }
        }
    }

    void RemoveEnemyBullet(GameObject[] enemyBullet)
    {
        for (int index = 0; index < enemyBullet.Length; index++)
        {
            if (enemyBullet[index].activeSelf)
                enemyBullet[index].SetActive(false);
        }
    }
    // 여기서 player가 Border에 닿고 있는지 switch문으로 판단
    // 이때 필요한 것이 앞서 정의한 isTouch 부분을 활용함
    // 다 연산된 isTouch들은 Update()쪽으로 가서 활용하게됨
    void OnTriggerEnter2D(Collider2D collision) // 플래그 채우기
    {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isRespawnTime)
                return;

            if (isHit)
                return;
            isHit = true;
            life--;
            GameManager.UpdateLifeIcon(life);
            GameManager.CallExplosion(transform.position, "P");

            if(life == 0)
            {
                GameManager.GameOver();
            }
            else
            {
                GameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Item")
        {
            ShootingItem item = collision.gameObject.GetComponent<ShootingItem>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if(power == maxPower)
                    {
                        score += 1000;
                    }
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;
                case "Boom":
                    if (boom == maxBoom)
                    {
                        score += 1000;
                    }
                    else
                    {
                        boom++;
                        GameManager.UpdateBoomIcon(boom); // 먹었을 때 남은 개수 반영
                    }
                    break;
            }
            collision.gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        if (power == 4)
        {
            followers[0].SetActive(true);
        }
        else if (power == 5)
        {
            followers[1].SetActive(true);
        }
        else if (power == 6)
        {
            followers[2].SetActive(true);
        }
    }
    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    // 이 부분은 isTouch에서 true로 바꾼 애들을 다시 false로 바꾸는 과정이다.
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}

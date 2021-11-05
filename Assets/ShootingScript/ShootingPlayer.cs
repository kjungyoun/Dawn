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

    // 총알 변수
    public GameObject bulletObjA;
    public GameObject bulletObjB;

    // 총알 나오는 횟수
    public float speed; // 비행기 속도
    public float power; // 파워
    public float maxShotDelay; // max에 알맞게 왔으면 다시 curShotDelay을 0으로 초기화하면서 쏘고
    public float curShotDelay; // 총알 쏘고 시간이 흐르고


    public int life;
    public int score;

    public ShootingGameManager shootingGameManager;

    public bool isHit;


    // Animator 초기화
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move(); // 이동
        Fire(); // 발사
        Reload(); // 장전
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position; // 현재 위치
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        switch (power)
        {
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
                break;
            case 3:
                GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.3f, transform.rotation);
                GameObject bulletCC = Instantiate(bulletObjB, transform.position, transform.rotation);
                GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.3f, transform.rotation);

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
        else if(collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            if (isHit)
                return;

            isHit = true;

            life--;
            shootingGameManager.UpdateLifeIcon(life);

            if(life == 0)
            {
                shootingGameManager.GameOver();
            }
            else
            {
                shootingGameManager.RespawnPlayer();
            }
            
            gameObject.SetActive(false); // 플레이어 비활성화!
            Destroy(collision.gameObject);
            // 플레이어를 활성화시키는(부활) 로직은 GameManager에서 관
        }
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

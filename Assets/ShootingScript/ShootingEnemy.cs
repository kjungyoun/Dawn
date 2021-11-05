using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
<<<<<<< HEAD

    public string enemyName;
    public float speed;
    public Sprite[] Sprites
        ;
    public int health;
    public int enemyScore;


    // 적의 총알 변수
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;

    public float maxShotDelay; // max에 알맞게 왔으면 다시 curShotDelay을 0으로 초기화하면서 쏘고
    public float curShotDelay; // 총알 쏘고 시간이 흐르고
=======
    public float speed;
    public int health;
    public Sprite[] Sprites;
>>>>>>> 7e932bb87f4f377b0aaa8a65e9a90a5a7692c45c

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

<<<<<<< HEAD
    void Update()
    {
        
        Fire(); // 발사
        Reload(); // 장전
    }

    void Fire()
    {

        if (curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        } else if (enemyName == "L")
        {
            GameObject bulletR = Instantiate(bulletObjB, transform.position + Vector3.right * 0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position + Vector3.left * 0.3f, transform.rotation);

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 2, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 2, ForceMode2D.Impulse);
        }

        curShotDelay = 0f;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = Sprites[1]; // 적이 내 총알을 맞았을 때, 흰색으로 변함
        Invoke("ReturnSprite", 0.1f);
        if (health <= 0)
        {
            ShootingPlayer playerLogic = player.GetComponent<ShootingPlayer>();
            playerLogic.score += enemyScore;
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = Sprites[0]; // 적이 내 총알을 맞았을 때, 다시 돌아옴
        spriteRenderer.sprite = Sprites[1]; // 적이 내 총알을 맞았을 때, 다시 돌아옴
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "PlayerBullet")
        {
            ShootingBullet bullet = collision.GetComponent<ShootingBullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }
    }
}

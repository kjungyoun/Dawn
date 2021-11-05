using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] Sprites;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = Sprites[1]; // 적이 내 총알을 맞았을 때, 흰색으로 변함
        Invoke("ReturnSprite", 0.1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
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

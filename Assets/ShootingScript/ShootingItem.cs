using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public string type;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rigid.velocity = Vector2.down * 1.5f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false); // Destroy는 게임 오브젝트를 아에 삭제하는 것임, 많이 쓰면 안좋음 // SetActive로 하는 것이 좋음
        }
    }
}

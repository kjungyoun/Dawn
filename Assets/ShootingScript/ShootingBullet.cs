using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public int dmg;
    // 총알이 경계선에 닿았을 때 삭제하기
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject); // Destroy는 게임 오브젝트를 아에 삭제하는 것임, 많이 쓰면 안좋음
        }
    }
}

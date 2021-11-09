using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFollower : MonoBehaviour
{
    public float maxShotDelay; // max에 알맞게 왔으면 다시 curShotDelay을 0으로 초기화하면서 쏘고
    public float curShotDelay; // 총알 빈도
    public int followDelay;

    public Vector3 followPos;
    public Transform parent;
    public Queue<Vector3> parentPos;

    public ShootingObjectManager objectManager; // 오브젝트 폴 담당

    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    void Update()
    {
        Watch();
        Follow(); // 이동
        Fire(); // 발사
        Reload(); // 장전
    }


    void Watch()
    {
        if(!parentPos.Contains(parent.position))
            parentPos.Enqueue(parent.position);

        if(parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
        else if (parentPos.Count < followDelay)
        {
            followPos = parent.position;
        }
    }


    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;

        GameObject bullet = objectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 15, ForceMode2D.Impulse);

        curShotDelay = 0f;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

}

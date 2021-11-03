using UnityEngine;
using System.Collections;

public class PacManPlayerController : MonoBehaviour
{

    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.right;
    private Direction direction = Direction.Right;

    private AroundWrap aroundWrap;
    private PacManMovementScript pacManMovementScript;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        tileLayer = 1 << LayerMask.NameToLayer("Tile");

        pacManMovementScript    = GetComponent<PacManMovementScript>();
        aroundWrap              = GetComponent<AroundWrap>();
        spriteRenderer          = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 1. 방향키 입력으로 이동방향 설정
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
            direction = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
            direction = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
            direction = Direction.Down;
        }

        // 2. 이동 방향에 광선 발사 (장애물 검사)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        // 2-1. 장애물이 없으면 이동
        if (hit.transform == null)
        {
            // MoveTo() 함수에 이동방향을 매개변수로 전달해 이동
            bool movePossible = pacManMovementScript.MoveTo(moveDirection);
            // 이동을 수행하게 되면
            if (movePossible)
            {
                // 오브젝트 회전 시킴 (애니메이션으로 처리해도 된다)
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }
            // 화면 밖으로 나가게 되면 반대편에서 등장
            aroundWrap.UpdateAroundWrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            // 아이템 획득 처리 (현재는 아이템을 파괴하기만 한다)
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            // 플레이어 캐릭터의 체력 감소 등 처리
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}

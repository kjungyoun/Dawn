using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PacManPlayerController : MonoBehaviour
{

    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.zero;
    private Direction direction = Direction.None;
    private int enemyNum = 3;
    private int sojuNum = 62;
    [SerializeField]
    private Image[] lifeImages;
    [SerializeField]
    private Text sojuCoin;

    private AroundWrap aroundWrap;
    private PacManMovementScript pacManMovementScript;
    private SpriteRenderer spriteRenderer;

    private FadeManager fadeManager;
    public Image image; // fade 용 Image
    public string sceneName; // 전환할 scene 이름



    [SerializeField]
    private VirtualJoystick virtualJoystick;

    private void Awake()
    {
        tileLayer = 1 << LayerMask.NameToLayer("Tile");

        pacManMovementScript = GetComponent<PacManMovementScript>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private void Update()
    {
        int a = virtualJoystick.getDirection();
        // 1. 방향키 입력으로 이동방향 설정
        if (a == 0)
        {
            moveDirection = Vector2.up;
            direction = Direction.Up;
            Debug.Log("up");
        }
        else if (a == 2)
        {
            moveDirection = Vector2.left;
            direction = Direction.Left;
            Debug.Log("Left");
        }
        else if (a == 3)
        {
            moveDirection = Vector2.right;
            direction = Direction.Right;
            Debug.Log("right");
        }
        else if (a == 1)
        {
            moveDirection = Vector2.down;
            direction = Direction.Down;
            Debug.Log("down");
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
            sojuNum -= 1;
            sojuCoin.text = sojuNum.ToString();
            Destroy(collision.gameObject);

            if(sojuNum == 0)
            {
                GameOver("SojuGameOverSceneSuccess");
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            // 플레이어 캐릭터의 체력 감소 등 처리

            enemyNum -= 1;
            UpdateLifeIcon();
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            Destroy(collision.gameObject);
            if (enemyNum == 0)
            {
                GameOver("SojuGameOverScene");
            }
        }

    }

    private void GameOver(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }

    private void UpdateLifeIcon()
    {
        for (int index = 0; index < 3; index++ )
        {
            lifeImages[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < enemyNum; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 1);
        }
    }

}

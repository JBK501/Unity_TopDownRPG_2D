using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    public GameManager manager;

    Rigidbody2D rigid;
    Animator anim;
    
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;
    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        // (대화창이 열렸으면 캐릭터 이동을 제한한다.)
        // 이동할 방향을 가져온다.
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        // 키 입력 상태를 확인한다.
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        // 수평이동 상태인 지 확인한다.
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // 이동 애니메이션을 처리한다.
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw")!=v)
        {
            anim.SetBool("isChange", true); 
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else // 한 쪽 방향키를 계속 누르고 있을 때는 Walk로 이동하는 트렌지션 처리를 없앤다.
            anim.SetBool("isChange", false); 

        // 키 다운 이벤트에 따라 방향 벡터(플레이어가 보는 방향)를 결정한다.
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if (hDown && h == 1)
            dirVec = Vector3.right;

        
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Action(scanObject);

    }

    void FixedUpdate()
    {
        // 이동한다.
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v); // 대각선 이동 방지
        rigid.velocity = moveVec * speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(1f,0f,0f));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }
}

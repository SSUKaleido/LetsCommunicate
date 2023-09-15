using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float MoveSpeed = 2f;
    public float JumpPower = 1f;

    Vector2 motionVectior;
    Animator animator;
    float LastHorizontalInputValue = 0;
    float HorizontalInputValue = 0;
    float VerticalInputValue = 0;

    bool isJumping = false; //점프한 후 공중에 있을 때
    bool isJumped = true; //점프를 했을 때 && 점프가 가능한지 여부

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalInputValue = Input.GetAxisRaw("Horizontal");
        VerticalInputValue = Input.GetAxisRaw("Vertical");
        //Debug.Log(VerticalInputValue);

        if (HorizontalInputValue != 0)
        {
            LastHorizontalInputValue = HorizontalInputValue;
        } //캐릭터가 보는 방향 유지하기 위해 마지막으로 입력받은 방향 기록

        
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(isJumping);
            isJumping = true;
        } //점프 활성화

        if (VerticalInputValue > 0)
        {
            Debug.Log(isJumping);
            isJumping = true;
        }
        
        //motionVectior = new Vector2(HorizontalInputValue, 0/*VerticalInputValue*/);
        animator.SetFloat("Horizontal", LastHorizontalInputValue);
        //animator.SetFloat("vertical", VerticalInputValue);

        /*
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("점프 키 눌렀음");
            Debug.Log(isJumping);
        }
        */
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVelocity = Vector3.left;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * MoveSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        if (isJumping == false)
            return;
        if (isJumped == false)
            return;

        //isJumping = true; //점프하면 isJumping을 true로
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, JumpPower);
        rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

        isJumped = false;

        Debug.Log(JumpPower);
        Debug.Log("점프했음");
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            isJumped = true;
            //땅에 닿으면 isJumping을 false로, 중복 점프 방지
            Debug.Log("땅에 닿았음");
        }
    }

}
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

    bool isJumping = false; //������ �� ���߿� ���� ��
    bool isJumped = true; //������ ���� �� && ������ �������� ����

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
        } //ĳ���Ͱ� ���� ���� �����ϱ� ���� ���������� �Է¹��� ���� ���

        
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(isJumping);
            isJumping = true;
        } //���� Ȱ��ȭ

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
            Debug.Log("���� Ű ������");
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

        //isJumping = true; //�����ϸ� isJumping�� true��
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, JumpPower);
        rigid.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

        isJumped = false;

        Debug.Log(JumpPower);
        Debug.Log("��������");
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            isJumped = true;
            //���� ������ isJumping�� false��, �ߺ� ���� ����
            Debug.Log("���� �����");
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;

    public int JumpCount = 1;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;

    //---------------------------------------------------[Override Function]
    //Initialization
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    //Graphic & Input Updates	
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log(isJumping);
            //Debug.Log(JumpCount);
            isJumping = true;
            //JumpCount--;
            //Debug.Log(JumpCount);
        }
    }

    //Physics engine Updates
    void FixedUpdate()
    {
        Move();
        Jump();
    }

    //---------------------------------------------------[Movement Function]

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

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;
        //if (JumpCount != 0)
        //    return;
        
        //Prevent Velocity amplification.
        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        Debug.Log(JumpCount);

        isJumping = false;
        //JumpCount = 1;
    }

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        JumpCount = 1;
    }
    */
}

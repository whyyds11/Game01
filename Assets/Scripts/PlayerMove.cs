using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float force_move = 50;
    public float jumpVelocity = 50;
    private Animator anim;
    private bool isGround = false;
    public bool isWall = false;//是否在墙上
    private Transform wallTrans;
    private bool isSlide = false;

    public GameObject particleSystem;
    private ParticleSystem particle;
    private bool isplay = false;

    private bool isOnceJump = false;
    private bool isDoubleJump = false;

    public float wallJumpXVelocity;
    private BoxCollider2D BC;
    Rigidbody2D rigid;
    Transform stampPoint;

    private int maxHealth = 5;
    private int currentHealth = 2;//当前生命值
    
    void Awake()
    {
        BC = GetComponent<BoxCollider2D>();
        anim = this.GetComponent<Animator>();
        particle = particleSystem.GetComponent<ParticleSystem>();
        stampPoint = transform.Find("StampPoint");
    }

    

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (isSlide == false)
        {

            if (h > 0.05f)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * force_move);
            }
            else if (h < -0.05f)
            {
                GetComponent<Rigidbody2D>().AddForce(-Vector2.right * force_move);
            }

            //修改方向
            if (h > 0.05f)//右
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (h < -0.05f)//左
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }


            anim.SetFloat("horizontal", Mathf.Abs(h));

            if(Input.GetKeyDown(KeyCode.Space))
            {
                isplay = true;
            }


            if (isGround && Input.GetKeyDown(KeyCode.Space))//跳跃
            {
                velocity.y = jumpVelocity;
                GetComponent<Rigidbody2D>().velocity = velocity;
                if (isWall)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                }

                print("一段跳");

                isOnceJump = true;
                isDoubleJump = false;
            }


            if (isOnceJump && Input.GetKeyDown(KeyCode.Space) && !isGround)//跳跃
            {
                velocity.y = jumpVelocity*1.5f;
                GetComponent<Rigidbody2D>().velocity = velocity;

                print("二段跳");

            //    anim.SetBool("isDoubleJumpUp",true);
                isDoubleJump = true;
                isOnceJump = false;
            }



            anim.SetFloat("vertical", GetComponent<Rigidbody2D>().velocity.y);

            anim.SetBool("isDoubleJump" , isDoubleJump);
            anim.SetBool("isOnceJump" , isOnceJump);

        }
        else   
        {
            //在墙上滑行时
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpVelocity;
                velocity.x = wallJumpXVelocity*transform.localScale.x;
                GetComponent<Rigidbody2D>().velocity = velocity;
                if (isWall)
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1;
                }

                print("一段跳");

                isOnceJump = true;
                isDoubleJump = false;
            }
        }
        if (isWall == false || isGround == true)
        {
            isSlide = false;
        }

        isPlayP();  

        print("isOnceJump" + isOnceJump);
        print("isDoubleJump" + isDoubleJump);
        
    }

    public void isPlayP()
    {
        if(isplay)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particle.Play();
            isplay = false;
       //     print("come here");
        }

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGround = true;
            GetComponent<Rigidbody2D>().gravityScale = 3;

        }
        if (col.collider.tag == "Wall")
        {
            isWall = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            wallTrans = col.collider.transform;
        }

        anim.SetBool("isGround", isGround);
        anim.SetBool("isWall", isWall);
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGround = false;
        }
        if (col.collider.tag == "Wall")
        {
            isWall = false;
            GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        anim.SetBool("isGround", isGround);
        anim.SetBool("isWall", isWall);
    }

    //更改朝向
    public void ChangeDir()
    {
        isSlide = true;
        if (wallTrans.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        StampTest();
        
    }

    private void StampTest()
    {
        Collider2D c = Physics2D.OverlapCircle(stampPoint.position, 0.1f, LayerMask.GetMask("Enemy"));
        if (c == null)
        {
            return;
        }

        //踩到了敌人
        Debug.Log("踩到敌人" + c.name);
        Destroy(c.gameObject);

       

        GetComponent<Rigidbody2D>().velocity = new Vector2(rigid.velocity.x,0);
        rigid.AddForce(new Vector2(0, 200));
    }
    
 
}
   


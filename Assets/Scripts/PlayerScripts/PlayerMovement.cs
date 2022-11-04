using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D Body;
    private Animator animator;
    public Transform groundChecker;
    public LayerMask groundLayer;
    private bool isGround;
    private float JumpHeight = 8.85f;

    void MovePlayer()
    {
        float side = Input.GetAxisRaw("Horizontal");      
        if(side > 0)
        {
            Body.velocity = new Vector2(speed, Body.velocity.y);
            ChangeDirection(1);
        }
        else if(side < 0)
        {
            Body.velocity = new Vector2(-speed, Body.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            Body.velocity = new Vector2(0f, Body.velocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)(Body.velocity.x)));
    }

    void ChangeDirection(int direction)
    {
        Vector3 temporaryScale = transform.localScale;
        temporaryScale.x = direction;
        transform.localScale = temporaryScale;
    }

    void PlayerJump()
    {
        isGround = Physics2D.Raycast(groundChecker.position, Vector2.down, 0.1f, groundLayer);
        if (isGround)
        {
            animator.SetBool("Jump", false);
        }
        if (isGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Body.velocity = new Vector2(Body.velocity.x, JumpHeight);
                animator.SetBool("Jump", true);
            }
        }
    }

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerJump();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
}

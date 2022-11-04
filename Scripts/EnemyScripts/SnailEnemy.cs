using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailEnemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform down_Collision, top_Collision, left_Collision, right_Collision;
    public LayerMask playerLayer;

    private Rigidbody2D Body;
    private Animator animator;
    private bool moveLeft;
    private bool canMove;
    private bool stunned;
    private Vector2 left_collision_position, right_collision_position;

    void SnailMove()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                Body.velocity = new Vector2(-speed, Body.velocity.y);
                ChangeDirection(0.4f);
            }
            else
            {
                Body.velocity = new Vector2(speed, Body.velocity.y);
                ChangeDirection(-0.4f);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 temporaryScale = transform.localScale;
        temporaryScale.x = direction;
        transform.localScale = temporaryScale;
    }

    void DetectCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.25f, playerLayer);

        if (topHit != null)
        {
            if (!stunned)
            {
                topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5.5f);
                canMove = false;
                Body.velocity = new Vector2(0, 0);
                animator.Play("DeadSnail");
                stunned = true;
            }
        }

        if (leftHit)
        {
            if(!stunned)
            {
                //PLAYER DMG
                print("Damage left");
            }
            else
            {
                Body.velocity = new Vector2(25f, 12f);
                StartCoroutine(Dead());
            }
        }

        if (rightHit)
        {
            if (!stunned)
            {
                //PLAYER DMG
                print("Damage right");
            }
            else
            {
                Body.velocity = new Vector2(-25f, 12f);
                StartCoroutine(Dead());
            }
        }

        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            moveLeft = !moveLeft;
            if (moveLeft)
            {
                left_Collision.localPosition = left_collision_position;
                right_Collision.localPosition = right_collision_position;
            }
            else
            {
                left_Collision.localPosition = right_collision_position;
                right_Collision.localPosition = left_collision_position;
            }
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            canMove = false;
            Body.velocity = new Vector2(0, 0);
            animator.Play("DeadSnail");
            stunned = true;
        }
    }

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        left_collision_position = left_Collision.localPosition;
        right_collision_position = right_Collision.localPosition;
    }

    void Start()
    {
        moveLeft = true;
        canMove = true;
    }


    void Update()
    {
        SnailMove();
        DetectCollision();
    }
}

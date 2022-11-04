using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D Body;
    private Animator animator;
    private Vector3 moveDirection = Vector3.down;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool canMove;


    void Move()
    {
        if(canMove == true)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

            if ((transform.position.y >= originPosition.y) || (Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer)))
            {
                moveDirection = moveDirection * -1;
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        Vector3 temporaryScale = transform.localScale;
        temporaryScale.x = temporaryScale.x * -1;
        transform.localScale = temporaryScale;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.BULLET_TAG)
        {
            canMove = false;
            animator.Play("SpiderDead");
            Body.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine("Dead");
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        originPosition = transform.position;;

        movePosition = transform.position;
        movePosition.y -= 4.5f;

        canMove = true;
    }


    void Update()
    {
        Move();
    }
}

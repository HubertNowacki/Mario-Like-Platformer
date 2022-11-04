using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    public float speed = 4f;
    public GameObject birdEgg;
    public LayerMask playerLayer;

    private Rigidbody2D Body;
    private Animator anim;
    private Vector3 moveDirection = Vector3.left;
    private Vector3 originPosition;
    private Vector3 movePosition;
    private bool hasEgg = true;
    private bool canMove = true;

    void Move()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);

            if ((transform.position.x >= originPosition.x)||(transform.position.x <= movePosition.x))
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

    void DropTheEgg()
    {
        if (hasEgg)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                hasEgg = false;
                anim.Play("Bird_NoStoneFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == MyTags.BULLET_TAG)
        {
            anim.Play("Bird_Dead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            Body.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            StartCoroutine(BirdDead());
        }
    }

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        originPosition = transform.position;
        originPosition.x += +6f;

        movePosition = transform.position;
        movePosition.x -= 6f;
    }

    void Update()
    {
        Move();
        DropTheEgg();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    public float jumpHeight = 7f;

    private Animator animator;
    private Rigidbody2D Body;
    private bool animation_Started;
    private bool animation_Finished;
    private bool jumpLeft;
    private int jumpAmount;


    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f);
    }

    void Awake()
    {
        animation_Started = false;
        animation_Finished = false;
        jumpLeft = true;
        jumpAmount = 10;
        animator = GetComponent<animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

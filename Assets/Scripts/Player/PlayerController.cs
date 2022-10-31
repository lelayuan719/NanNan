using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : GenericController
{
    public float speed;
    private Rigidbody2D rb;
    private float inputHorizontal;
    private float inputVertical;
    public float distance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    private float distWalked;
    public SpriteRenderer sr;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isWalking",false);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG zooming
        if (Input.GetKeyDown("]"))
        {
            speed *= 5;
        }
        else if (Input.GetKeyDown("["))
        {
            speed /= 5;
        }
    }

    void FixedUpdate(){
        if (playerCanMove)
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
            if (inputHorizontal != 0)
            {
                anim.SetBool("isWalking", true);
                if (inputHorizontal < 0)
                {
                    sr.flipX = true;
                }
                else
                {
                    sr.flipX = false;
                }
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
    }

    public void MoveTo(Transform destination, UnityEvent onComplete)
    {
        StartCoroutine(MoveTo(destination.position, onComplete));
    }

    public void MoveTo(Transform destination)
    {
        StartCoroutine(MoveTo(destination.position, null));
    }

    IEnumerator MoveTo(Vector2 destination, UnityEvent onComplete)
    {
        // Check for already there
        if (destination.x == transform.position.x)
        {
            yield break;
        }

        playerCanMove = false;
        anim.SetBool("isWalking", true);
        float directionToMove = Mathf.Sign(destination.x - transform.position.x);

        // Flip
        if (directionToMove < 0)
            sr.flipX = true;
        else
            sr.flipX = false;

        // Move while we are still on the same side
        while (Mathf.Sign(destination.x - transform.position.x) == directionToMove)
        {
            rb.velocity = new Vector2(directionToMove * speed, rb.velocity.y);
            yield return new WaitForFixedUpdate();
        }
        transform.position = new Vector2(destination.x, transform.position.y);

        // Reset and do callback
        playerCanMove = true;
        anim.SetBool("isWalking", false);
        if (onComplete != null) onComplete.Invoke();
    }
}

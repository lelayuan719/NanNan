using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ClimbingController))]
public class PlayerController : GenericController
{
    public float speed;
    public float distance;
    public LayerMask whatIsLadder;

    private Rigidbody2D rb;
    private ClimbingController climbCtrl;
    private SpriteRenderer sr;
    private Collider2D collide;
    private Animator anim;

    private float inputHorizontal;
    private float inputVertical;
    private bool touchingGround;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        climbCtrl = GetComponent<ClimbingController>();

        anim.SetBool("isWalking",false);
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG fast speed
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
        if (playerCanMove && CanMoveHorizClimbing())
        {
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
            if (inputHorizontal != 0)
            {
                anim.SetBool("isTalking", false);
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

    // Determines if we can move horizontally while climbing
    // True if we're not climbing or we're at the bottom
    bool CanMoveHorizClimbing()
    {
        if (climbCtrl.isClimbing && !touchingGround)
        {
            return false;
        }

        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
        }
    }

    public void TeleportTo(Vector3 destPos)
    {
        GameManager.GM.cine.ActiveVirtualCamera.OnTargetObjectWarped(transform, destPos - transform.position);
        transform.position = destPos;
    }

    public void TeleportTo(Transform destination)
    {
        // This makes sure we can still raycast through NanNan
        Vector3 destPos = new Vector3(destination.position.x, destination.position.y, 1);
        TeleportTo(destPos);
    }

    public void MoveTo(Transform destination, UnityEvent onComplete, bool canMoveAfter)
    {
        StartCoroutine(MoveTo(destination.position, onComplete, canMoveAfter));
    }

    public void MoveTo(Transform destination, bool canMoveAfter)
    {
        StartCoroutine(MoveTo(destination.position, null, canMoveAfter));
    }

    IEnumerator MoveTo(Vector2 destination, UnityEvent onComplete, bool canMoveAfter)
    {
        // Check for already there
        if (destination.x == transform.position.x)
        {
            if (onComplete != null) onComplete.Invoke();
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
        if (canMoveAfter) playerCanMove = true;
        anim.SetBool("isWalking", false);
        if (onComplete != null) onComplete.Invoke();
    }

    public override void FreezeCharacter()
    {
        base.FreezeCharacter();
        GetComponent<Animator>().SetBool("isWalking", false);
    }
}

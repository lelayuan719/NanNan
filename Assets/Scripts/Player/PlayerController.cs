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

    Rigidbody2D rb;
    ClimbingController climbCtrl;
    SpriteRenderer sr;
    Animator anim;

    [HideInInspector] public bool instantDoors;
    float inputHorizontal;
    float inputVertical;
    bool touchingGround;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        climbCtrl = GetComponent<ClimbingController>();
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
            TryMove(inputHorizontal);
        }
    }

    public void TryMove(float direction)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        if (direction != 0)
        {
            anim.SetBool("isTalking", false);
            anim.SetBool("isWalking", true);
            if (direction < 0)
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

    public void SetInstantDoors(bool instantDoors)
    {
        this.instantDoors = instantDoors;
    }
}

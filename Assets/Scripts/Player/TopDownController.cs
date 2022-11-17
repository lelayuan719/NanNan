using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : GenericController
{
    public float speed = 5.0f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

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

        if (!playerCanMove) return;

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(inputHorizontal * speed, inputVertical * speed);
        if (rb.velocity != Vector2.zero)
        {
            anim.SetBool("isWalking", true);

            if (inputVertical > 0)
            {
                anim.SetInteger("direction", 1);
            }
            else if (inputVertical < 0)
            {
                anim.SetInteger("direction", 3);
            }
            else
            {
                if (inputHorizontal > 0)
                {
                    anim.SetInteger("direction", 2);
                }
                else
                {
                    anim.SetInteger("direction", 4);
                }
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetInteger("direction", 0);
        }
    }

    public override void FreezeCharacter()
    {
        base.FreezeCharacter();
        GetComponent<Animator>().SetInteger("direction", 0);
    }
}

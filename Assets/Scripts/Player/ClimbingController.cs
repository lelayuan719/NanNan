using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingController : MonoBehaviour
{
    private float vertical;
    private float speed = 80f;
    private bool isLadder;

    private Rigidbody2D rb;
    private Animator anim;
    private PlayerController normalController;

    public bool isClimbing;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        normalController = GetComponent<PlayerController>();

        anim.SetBool("isClimbing",false);
    }
    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f){
            isClimbing = true;
        }
    }

    private void FixedUpdate(){
        if (!normalController.playerCanMove)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, 0);
        }
        else if (isClimbing) {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, vertical * speed);
        } else {
            rb.gravityScale = 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            isLadder = true;
            anim.SetBool("isClimbing",true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Ladder")){
            isLadder = false;
            isClimbing = false;
            anim.SetBool("isClimbing",false);
        }
    }
}

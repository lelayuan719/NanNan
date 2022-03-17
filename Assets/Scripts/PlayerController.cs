using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float inputHorizontal;
    private float inputVertical;
    public float distance;
    public LayerMask whatIsLadder;
    private bool isClimbing;
    private float distWalked;
    public bool playerCanMove = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isWalking",false);
        distWalked = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){
        if (playerCanMove){
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
            distWalked += inputHorizontal*speed;
            if(inputHorizontal!=0){
                anim.SetBool("isWalking",true);
            } else {
                anim.SetBool("isWalking",false);
            }
            
            if (distWalked > 200000 || distWalked < -5000){
                rb.velocity = new Vector2(0, rb.velocity.y);
                distWalked -= inputHorizontal*speed;
                //Instantiate(gameObject,mountain);
            }
        } else {
            anim.SetBool("isWalking",false);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
        if(inputHorizontal!=0){
            anim.SetBool("isWalking",true);
        } else {
            anim.SetBool("isWalking",false);
        }
        
        if (rb.transform.position.x < 300 && inputHorizontal < 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        } else if (rb.transform.position.x > 4050 && inputHorizontal > 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
            SceneManager.LoadScene("Chapter 1", LoadSceneMode.Single);
        }
    }
}

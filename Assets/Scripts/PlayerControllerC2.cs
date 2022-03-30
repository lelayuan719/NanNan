using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerC2 : MonoBehaviour
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
    public int left;
    public int right;
    public int upper;
    public int lower;
    public int padding;
    public string NextScene;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        anim.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);
        if (inputHorizontal != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (rb.transform.position.x < left && inputHorizontal < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (rb.transform.position.x > right && inputHorizontal > 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);
    }
}

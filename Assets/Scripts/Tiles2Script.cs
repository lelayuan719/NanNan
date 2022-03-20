using UnityEngine;

public class Tiles2Script : MonoBehaviour
{
    public Vector3 destPosition;
    private Vector3 corrPosition;
    private SpriteRenderer spr;
    public int tileNum; 
    public bool finalPosition;

    void Awake()
    {
        destPosition = transform.position;
        corrPosition = transform.position;
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(a: transform.position, b: destPosition, t: 0.1f);
        if (destPosition == corrPosition){
            //spr.color = Color.green;
            finalPosition = true;
        }
        else {
            //spr.color = Color.white;
            finalPosition = false;
        }
    }
}

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float upper;
    public float right;
    public float left;
    public float lower;
    public Transform player;
    public Vector3 offset;
    [Range(0,5)]
    public float smoothFactor;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerPosition = player.position + offset;
        

        if (playerPosition.x > right){
            playerPosition.x = right;
        } else if (playerPosition.x < left){
            playerPosition.x = left;
        }

        if (playerPosition.y > upper){
            playerPosition.y = upper;
        }
        Vector3 smoothedPosition = 
            Vector3.Lerp(transform.position,
            playerPosition,smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}

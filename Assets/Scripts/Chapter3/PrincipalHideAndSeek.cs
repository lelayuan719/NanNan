using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrincipalHideAndSeek : MonoBehaviour
{
    [SerializeField] float seekSpeed = 3;
    [SerializeField] float chaseSpeed = 6;
    [SerializeField] float moveThreshold = 0.2f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float sightRange;
    [SerializeField] LayerMask playerLayer;
    public List<PrincipalNode> _nodes;
    [SerializeField] string startNodeId;

    bool started = false;
    States state = States.Seeking;
    PrincipalNode nodeDest;
    Rigidbody2D rb;
    HidingController playerHide;
    Dictionary<string, PrincipalNode> nodes;
    float direction = +1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize nodes
        foreach (var node in _nodes)
        {
            node.Init();
        }
        nodes = _nodes.ToDictionary(x => x.id, x => x);

        nodeDest = nodes[startNodeId];

        print(nodeDest.transform.position);

        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return new WaitForEndOfFrame();
        playerHide = GameManager.GM.player.GetComponent<HidingController>();
    }


    public void StartSeeking()
    {
        started = true;
    }

    void Update()
    {
        if (!started) return;

        switch (state)
        {
            case States.Seeking:
                DoSeekState();
                break;
            case States.Chasing:
                DoChaseState();
                break;
            case States.EnteringDoor:
                DoEnteringDoorState();
                break;
            case States.Waiting:
                DoWaitingState();
                break;
        }
    }

    void DoSeekState()
    {
        // Check for moving to door / waiting
        Vector2 vectorTo = nodeDest.transform.position - transform.position;
        if (Mathf.Abs(vectorTo.x) < moveThreshold)
        {
            string doorDest = nodeDest.GetDoorDestination();
            rb.velocity = Vector2.zero;

            if (doorDest != null)
            {
                state = States.EnteringDoor;
                StartCoroutine(DoDoorStateCR());
                return;
            }
            else
            {
                state = States.Waiting;
                StartCoroutine(DoWaitingStateCR());
                return;
            }
        }

        // Check for seeing player
        if (CanSeePlayer())
        {
            state = States.Chasing;
            return;
        }

        // Move
        direction = Mathf.Sign(vectorTo.x);
        rb.velocity = new Vector2(direction * seekSpeed, 0);
    }

    void DoChaseState()
    {
        // Checks for seeing player
        if (!CanSeePlayer())
        {
            state = States.Seeking;
            return;
        }

        Vector2 vectorTo = GameManager.GM.player.transform.position - transform.position;
        direction = Mathf.Sign(vectorTo.x);

        rb.velocity = new Vector2(direction * chaseSpeed, 0);
    }

    bool CanSeePlayer()
    {
        // Hiding means player is invisible, unless principal is in chase state
        if (playerHide.hiding && state != States.Chasing) return false;

        // See if player is in LOS
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, sightRange, playerLayer);
        if ((hit.collider != null) && hit.transform.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    void DoEnteringDoorState()
    {
        
    }

    IEnumerator DoDoorStateCR()
    {
        // Wait in front of door
        yield return new WaitForSeconds(waitTime);

        // Teleport and reset nodes
        nodeDest.transform.gameObject.GetComponent<DoorTeleport2>().TeleportSomething(gameObject);

        PrincipalNode newNode = nodes[nodeDest.GetDoorDestination()];
        nodeDest = nodes[newNode.GetWalkDestination()];

        // Wait on other side of door
        yield return new WaitForSeconds(waitTime);

        // Change state
        state = States.Seeking;
    }

    void DoWaitingState()
    {
        
    }

    IEnumerator DoWaitingStateCR()
    {
        yield return new WaitForSeconds(waitTime);

        nodeDest = nodes[nodeDest.GetWalkDestination()];
        state = States.Seeking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction * sightRange, 0));
    }

    enum States
    {
        Seeking,
        Chasing,
        EnteringDoor,
        Waiting,
    }

    [System.Serializable]
    public class PrincipalNode
    {
        public string id;
        public Transform transform;
        public List<PrincipalEdge> edges;

        string doorDest;
        List<string> walkDest = new List<string>();

        public void Init()
        {
            foreach (var edge in edges)
            {
                if (edge.traversalType == PrincipalEdge.TraversalTypes.Door)
                {
                    doorDest = edge.to;
                }

                else if (edge.traversalType == PrincipalEdge.TraversalTypes.Walk)
                {
                    walkDest.Add(edge.to);
                }
            }
        }

        public string GetDoorDestination()
        {
            return doorDest;
        }

        public string GetWalkDestination()
        {
            return walkDest[Random.Range(0, walkDest.Count)];
        }
    }

    [System.Serializable]
    public class PrincipalEdge
    {
        public string to;
        public TraversalTypes traversalType;

        public enum TraversalTypes
        {
            Walk,
            Door,
        }
    }
}
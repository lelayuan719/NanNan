using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

public class PrincipalHideAndSeek : MonoBehaviour
{
    [SerializeField] float seekSpeed = 3;
    [SerializeField] float chaseSpeed = 6;
    [SerializeField] float memoryTime = 2;
    [SerializeField] float moveThreshold = 0.2f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float sightRange;
    [SerializeField] LayerMask playerLayer;
    public List<PrincipalNode> _nodes;
    [ResizableTextArea] [SerializeField] string edges;
    [SerializeField] string[] startNodeIdOptions;
    [ShowNonSerializedField] string goingTo = "";

    float speed;

    enum State
    {
        Seeking,
        Chasing,
        Aware,
        EnteringDoor,
        Waiting,
    }
    State state = State.Seeking;

    PrincipalNode nodeDest;
    GameObject principal;
    Rigidbody2D rb;
    HidingController playerHide;
    Coroutine awareCr;

    Dictionary<string, PrincipalNode> nodes;
    bool started = false;
    float direction = +1;

    private void Awake()
    {
        // Initialize nodes
        var connections = ProcessEdges(_nodes, edges);
        foreach (var node in _nodes)
        {
            node.Init(connections[node.id]);
        }
        nodes = _nodes.ToDictionary(x => x.id, x => x);

        speed = seekSpeed;

        ResetState();
    }

    void Start()
    {
        principal = transform.parent.gameObject;
        rb = principal.GetComponent<Rigidbody2D>();

        StartCoroutine(AfterStart());
    }

    IEnumerator AfterStart()
    {
        yield return new WaitForEndOfFrame();
        playerHide = GameManager.GM.player.GetComponent<HidingController>();
    }

    public void ResetState()
    {
        string startNodeId = startNodeIdOptions.RandomElement();
        ChangeDest(startNodeId);
        state = State.Seeking;
    }

    public void ChangePlayerLocation(string nodeId)
    {
        // If we're chasing the player, follow the player into a door
        if (state == State.Chasing)
        {
            ChangeDest(nodeId);
        }
    }

    void ChangeDest(string nodeId)
    {
        nodeDest = nodes[nodeId];
        goingTo = nodeId;
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
            case State.Seeking:
                DoSeekState();
                break;
            case State.Chasing:
                DoChaseState();
                break;
            case State.Aware:
                DoAwareState();
                break;
            case State.EnteringDoor:
                DoEnteringDoorState();
                break;
            case State.Waiting:
                DoWaitingState();
                break;
        }
    }

    void ChangeState(State newState)
    {
        switch (newState)
        {
            // Ending at seeking
            case State.Seeking:
                speed = seekSpeed;
                break;

            // Ending at chasing
            case State.Chasing:
                speed = chaseSpeed;
                break;
                
            // Ending at aware
            case State.Aware:
                speed = chaseSpeed;
                awareCr = StartCoroutine(AwareRevertState());
                break;

            // Ending at entering door
            case State.EnteringDoor:
                StartCoroutine(DoDoorStateCR());
                break;

            // Ending at waiting
            case State.Waiting:
                StartCoroutine(DoWaitingStateCR());
                break;
        }

        // Stopping aware coroutine if necessary
        if (state != State.Aware)
        {
            if (awareCr != null) StopCoroutine(awareCr);
        }

        // Changing state
        state = newState;
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
                ChangeState(State.EnteringDoor);
                return;
            }
            else
            {
                ChangeState(State.Waiting);
                return;
            }
        }

        // Check for seeing player
        if (CanSeePlayer())
        {
            ChangeState(State.Chasing);
            return;
        }

        // Move
        direction = Mathf.Sign(vectorTo.x);
        rb.velocity = new Vector2(direction * speed, 0);
    }

    void DoChaseState()
    {
        // Checks for seeing player
        if (!CanSeePlayer())
        {
            ChangeState(State.Aware);
            return;
        }

        Vector2 vectorTo = GameManager.GM.player.transform.position - transform.position;
        direction = Mathf.Sign(vectorTo.x);

        rb.velocity = new Vector2(direction * speed, 0);
    }

    bool CanSeePlayer()
    {
        // Hiding means player is invisible, unless principal is in chase state
        if (playerHide.hiding && state != State.Chasing) return false;

        // See if player is in LOS
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, sightRange, playerLayer);
        if ((hit.collider != null) && hit.transform.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    // Aware is seeking but faster
    void DoAwareState()
    {
        DoSeekState();
    }

    // Changes to seeking after some time
    IEnumerator AwareRevertState()
    {
        yield return new WaitForSeconds(memoryTime);
        ChangeState(State.Seeking);
    }

    void DoEnteringDoorState()
    {
        
    }

    IEnumerator DoDoorStateCR()
    {
        // Wait in front of door
        yield return new WaitForSeconds(waitTime);

        // Teleport and reset nodes
        nodeDest.transform.gameObject.GetComponent<DoorTeleport2>().TeleportSomething(principal.gameObject);

        PrincipalNode newNode = nodes[nodeDest.GetDoorDestination()];
        string nodeId = newNode.GetWalkDestination();
        ChangeDest(nodeId);

        // Wait on other side of door
        yield return new WaitForSeconds(waitTime);

        // Change state
        ChangeState(State.Seeking);
    }

    void DoWaitingState()
    {
        
    }

    IEnumerator DoWaitingStateCR()
    {
        yield return new WaitForSeconds(waitTime);

        string nodeId = nodeDest.GetWalkDestination();
        ChangeDest(nodeId);
        ChangeState(State.Seeking);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (state == State.Chasing && collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<HideAndSeekReset>().ResetScene();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction * sightRange, 0));
    }

    Dictionary<string, List<PrincipalDestination>> ProcessEdges(List<PrincipalNode> nodes, string rawEdges)
    {
        var connections = nodes.ToDictionary(x => x.id, x => new List<PrincipalDestination>());

        // Each line is an edge group of "TT node1 node2"
        // TT is traversal type
        // Following nodes are all connected
        string[] edgeGroups = rawEdges.Split('\n');
        foreach (string edgeGroup in edgeGroups)
        {
            string[] edgeTokens = edgeGroup.Split(' ');

            // Get traversal type
            PrincipalDestination.TraversalType? traversalType = PrincipalDestination.StrToTT(edgeTokens[0]);
            if (traversalType == null)
            {
                Debug.LogErrorFormat("Invalid traversal type '{0}'", edgeTokens[0]);
                continue;
            }

            // Connect nodes
            string[] nodesInGroup = edgeTokens[1..];
            List<List<string>> edges = GetPermutations(nodesInGroup, 2);
            foreach (var edge in edges)
            {
                string from = edge[0];
                string to = edge[1];

                var dest = new PrincipalDestination(to, (PrincipalDestination.TraversalType)traversalType);
                connections[from].Add(dest);
            }
        }

        return connections;
    }

    public static List<List<string>> GetPermutations(string[] arr, int n)
    {
        List<List<string>> permutations = new List<List<string>>();
        if (n == 1)
        {
            foreach (string e in arr)
            {
                permutations.Add(new List<string>() { e });
            }
        }
        else
        {
            foreach (string e in arr)
            {
                List<string> newArr = arr.Where(x => x != e).ToList();
                List<List<string>> subPermutations = GetPermutations(newArr.ToArray(), n - 1);
                foreach (List<string> subPermutation in subPermutations)
                {
                    subPermutation.Insert(0, e);
                    permutations.Add(subPermutation);
                }
            }
        }

        return permutations;
    }

    [System.Serializable]
    public class PrincipalNode
    {
        public string id;
        public Transform transform;

        string doorDest;
        List<string> walkDest = new List<string>();

        public void Init(IEnumerable<PrincipalDestination> destinations)
        {
            foreach (var destination in destinations)
            {
                if (destination.traversalType == PrincipalDestination.TraversalType.Door)
                {
                    doorDest = destination.to;
                }

                else if (destination.traversalType == PrincipalDestination.TraversalType.Walk)
                {
                    walkDest.Add(destination.to);
                }
            }
        }

        public string GetDoorDestination()
        {
            return doorDest;
        }

        public string GetWalkDestination()
        {
            return walkDest.RandomElement();
        }
    }

    public class PrincipalDestination
    {
        public string to;
        public TraversalType traversalType;

        public enum TraversalType
        {
            Walk,
            Door,
        }

        public PrincipalDestination(string to, TraversalType traversalType)
        {
            this.to = to;
            this.traversalType = traversalType;
        }

        public static TraversalType? StrToTT(string traversalType)
        {
            if (traversalType == "WALK")
            {
                return TraversalType.Walk;
            }
            else if (traversalType == "DOOR")
            {
                return TraversalType.Door;
            }
            else
            {
                return null;
            }
        }
    }
}
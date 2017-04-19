using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAI : MonoBehaviour
{
    //General state machine variables
    private Animator anim;

    //Track location
    public GameObject player;
    private Ray ray;
    private RaycastHit hit;
    private float maxDistanceToCheck = 6.0f;
    private float currentDistance;
    private Vector3 checkDirection;

    //Patrol state
    public Transform pointA;
    public Transform pointB;
    public NavMeshAgent agent;

    private int currentTarget;
    private float distanceFromTarget;
    private Transform[] waypoints = null;

    //Animator variables
    const string S_Patrol = "Patrol";
    const string S_Chase = "Chase";
    const string S_Shoot = "Shoot";

    const string P_DistanceFromPlayer = "distanceFromPlayer";
    const string P_PlayerVisible = "isPlayerVisible";
    const string P_DistanceFromWaypoint = "distanceFromWaypoint";

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        pointA = GameObject.Find("p1").transform;
        pointB = GameObject.Find("p2").transform;

        agent = GetComponent<NavMeshAgent>();

        waypoints = new Transform[2] { pointA, pointB };

        currentTarget = 0;

        agent.SetDestination(waypoints[currentTarget].position);
        
    }

	
	// Update is called once per frame
	void FixedUpdate ()
    {
        currentDistance = Vector3.Distance(player.transform.position, transform.position);
        anim.SetFloat(P_DistanceFromPlayer, currentDistance);

        checkDirection = player.transform.position - transform.position;
        ray = new Ray(transform.position, checkDirection);

        if(Physics.Raycast(ray.origin, ray.direction, out hit, maxDistanceToCheck))
        {
            if (hit.transform.CompareTag("Player"))
                anim.SetBool(P_PlayerVisible, true);
            else anim.SetBool(P_PlayerVisible, false);
        }
        else anim.SetBool(P_PlayerVisible, false);

        distanceFromTarget = Vector3.Distance(waypoints[currentTarget].position, transform.position);
        anim.SetFloat(P_DistanceFromWaypoint, distanceFromTarget);
    }

    public void SetNextPoint()
    {
        switch(currentTarget)
        {
            case 0:
                currentTarget = 1;
                break;
            case 1:
                currentTarget = 0;
                break;
        }

        agent.SetDestination(waypoints[currentTarget].position);
    }
}

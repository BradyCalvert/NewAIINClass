using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour {
    const string S_WANDER = "Wander";
    const string S_CHASE = "Chase";

    const string P_PLAYER_ALIVE = "PlayerAlive";
    const string P_Player_IN_Range = "TooFar";
    const string P_Player_Seen = "PlayerVisible";



    //references
    public GameObject player;
    Animator anim;
    public float chaseRange;


    public float checkFrequency;


    public void Awake()
    {

        anim = GetComponent<Animator>();
        player = GameManager.instance.player;
        InvokeRepeating("CheckForPlayerDeath", checkFrequency, checkFrequency);
    }


    void Update()
    {
        CheckForPlayerDeath();
        CheckForPlayerRange();
#if UNITY_EDITOR
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction.normalized * chaseRange);
#endif
    }

    void CheckIfPlayerIsVisible()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, chaseRange))
        {
            if (hit.transform.CompareTag("Player"))
            {
                anim.SetBool(P_Player_Seen, true);
            }

            else

                anim.SetBool(P_Player_Seen, false);
        }
        else
        {
            anim.SetBool(P_Player_Seen, false);
        }

    }



    public void CheckForPlayerRange()
    {

        float distance = Vector3.Distance(player.transform.position,transform.position);
        {
            if (distance < chaseRange)
            {
                anim.SetBool(P_Player_IN_Range, true);
                CheckIfPlayerIsVisible();
            }
            else
            {
                anim.SetBool(P_Player_IN_Range, false);
            }
        }
    }





        void CheckForPlayerDeath()
    {
            if (GameManager.instance.playerHealth <= 0)
            {
                anim.SetBool(P_PLAYER_ALIVE, false);
            }
            else
                anim.SetBool(P_PLAYER_ALIVE, true);
        }


        void OnDrawGizmos()
    {
        if (chaseRange > 0)
        {

            Gizmos.color = Color.cyan;
        }
            Gizmos.DrawWireSphere(transform.position, chaseRange);

        }
    }



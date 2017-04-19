using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateEnemyChaseV2 : StateMachineBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameManager.instance.player;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        agent.SetDestination(player.transform.position);
    }


}


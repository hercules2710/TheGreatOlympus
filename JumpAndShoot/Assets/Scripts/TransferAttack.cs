using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferAttack : StateMachineBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float attackRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("camPlayer");
        rb = animator.GetComponent<Rigidbody>();
    }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.Distance(player.transform.position,rb.position) <= attackRange)
        {
            animator.SetBool("Attack",true);
           // Debug.Log("hurt");
        }
    }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    protected EnemyState currentState;
    public NavMeshAgent agent;
    protected Transform tf;
    protected Rigidbody rb;
    public Animator anim;



    public virtual void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tf = transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


}

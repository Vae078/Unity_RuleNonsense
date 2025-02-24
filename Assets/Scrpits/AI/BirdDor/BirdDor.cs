using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdDor : Enemy
{

    public Transform navi_1;
    public Transform navi_2;
    public Transform navi_3;
    


    //-----------状态机-------------
    public BirdDor_Idle idleState;
    public BirdDor_Check checkState;
    public BirdDor_FindPlayer findState;
    public BirdDor_FindPlayer findPlayerState;


    private void Awake()
    {
        idleState = new BirdDor_Idle(this);
        checkState = new BirdDor_Check(this);
        findState = new BirdDor_FindPlayer(this);
        findPlayerState = new BirdDor_FindPlayer(this, true);
    }


    public override void Start()
    {
        base.Start();
        currentState = idleState;
        //currentState = findState;
        currentState.Enter();
    }


    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        
        if (agent.isStopped)
        {
            //防止停下来还在乱转
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;

        }

        currentState.Update();

    }

    public override void ChangeState(EnemyState newState)
    {
        base.ChangeState(newState);
        
    }


   //public void OnDoorTouchChange(bool isDoorTouch)
   // {
   //     if (isDoorTouch)
   //     {
   //         Debug.Log("检测到玩家开门");
   //         ChangeState(findState);
   //     }
   // }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 这个是鸟嘴医生的idle状态
/// 负责巡逻 巡逻用navigation实现
/// 巡逻--停下--巡逻
/// </summary>
public class BirdDor_Idle : EnemyState
{

    private BirdDor birdDor;
    private bool isArriveNavi1 = false;
    private bool isStop;
    private float Timer;
    public GameState doorState = GameState.isDoorTouch;

    public BirdDor_Idle(BirdDor _birdDor)
    {
        birdDor = _birdDor;
    }

    public void Enter()
    {
        //进入状态是，将doorstate设置为false
       // GameRoot.GetInstacne().SubtitleControl_BirDor("测试测试测试测试");

        isArriveNavi1 = false;
        isStop = false;
        StateDetector.Instance.SetState(doorState, false);
        birdDor.agent.SetDestination(birdDor.navi_1.position);
        //注册状态监听
        StateDetector.Instance.SubscribeToState(doorState, OnDoorTouchChange);
    }

    public void Update()
    {
        Patrol();
    }

    public void Exit()
    {
        //取消监听
        StateDetector.Instance.UnsubscribeFromState(doorState, OnDoorTouchChange);
    }


    public void Patrol()
    {
        
        if (!isStop&&!birdDor.agent.pathPending && birdDor.agent.remainingDistance < 0.2f)
        {
            isStop = true;
            birdDor.StartCoroutine(IE_idle());
        }
    }

    IEnumerator IE_idle()
    {
        birdDor.agent.isStopped = true;
        yield return new WaitForSeconds(3.0f);
        birdDor.agent.isStopped = false;
        
        if (isArriveNavi1)
        {
            birdDor.agent.SetDestination(birdDor.navi_2.position);
            
        }
        else
            birdDor.agent.SetDestination(birdDor.navi_1.position);
        
        isArriveNavi1 = !isArriveNavi1;
        isStop = false;

    }
    public void OnDoorTouchChange(bool isDoorTouch)
    {
        if (isDoorTouch)
        {
            
            birdDor.ChangeState(birdDor.findState);
            GameRoot.GetInstacne().SubtitleControl_BirDor("BirdDor要来哩 测试测试测试测试");
        }
    }
}

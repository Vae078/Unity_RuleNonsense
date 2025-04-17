using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 鸟嘴医生检查状态 观察者
/// 进入此状态时订阅所有规则状态，同时获取所有规则状态当前的bool值
/// 以当前的规则状态值作为判断条件
/// 确保了所有规则的同步
/// </summary>
public class BirdDor_Check :EnemyState
{
    private BirdDor birdDor;
    public GameState eatState = GameState.isEatMedicine;
    public GameState hideState = GameState.isMedicineDestory;
    public GameState cleanState = GameState.isRoomClean;

    bool eatMedice;
    bool clean;
    bool hide;
    bool kill;
    bool cup;
    bool t;

    public BirdDor_Check(BirdDor _birdDor)
    {
        birdDor = _birdDor;
    }


    public void Enter()
    {
        birdDor.agent.isStopped = false;

        StateDetector.Instance.SubscribeToState(eatState, isPlayerEatMedicine);
        StateDetector.Instance.SubscribeToState(hideState,isPlayerHideMedicine);
        StateDetector.Instance.SubscribeToState(cleanState, isRoomClean);
        StateDetector.Instance.SubscribeToState(GameState.isCupRight, isCupRight);
        StateDetector.Instance.SubscribeToState(GameState.isThermometerRight, isThermometerRight);
    }

    public void Update()
    {

        Debug.Log("我现在在CheckState");

        //if (StateDetector.Instance.GetState(hideState) == true && StateDetector.Instance.GetState(cleanState) == true)
        //{
        //    Debug.Log("检查通过，你做的很好");
        //    birdDor.StartCoroutine(waitChangeState(birdDor.idleState));
        //    //birdDor.ChangeState(birdDor.idleState);
        //}
        
        Debug.Log("Clean is " + clean);

        if (kill)
        {
            KillPlayer();
        }

    }

    public void Exit()
    {
        StateDetector.Instance.UnsubscribeFromState(eatState, isPlayerEatMedicine);
        StateDetector.Instance.UnsubscribeFromState(hideState, isPlayerHideMedicine);
        StateDetector.Instance.UnsubscribeFromState(cleanState, isRoomClean);
        StateDetector.Instance.UnsubscribeFromState(GameState.isCupRight, isCupRight);
        StateDetector.Instance.UnsubscribeFromState(GameState.isThermometerRight,isThermometerRight);
        birdDor.agent.isStopped = false;

    }

    //这些是事件响应函数
    private void isPlayerEatMedicine(bool _EatMedicine)  //玩家吃药
    {
        eatMedice = _EatMedicine;
    }
    
    private void isPlayerHideMedicine(bool _hide)    //玩家摧毁了药（藏起来或吃了）
    {
        hide = _hide;
        Debug.Log("hide is " + hide);

    }

    private void isCupRight(bool _cup)
    {
        cup = _cup;
        Debug.Log("cup is " + cup);
    }

    private void isThermometerRight(bool _t)
    {
        t = _t;
        Debug.Log("t is " + t);
        if (cup && t)
        {
            clean = true;
        }

        /*
         * 在Enter到Cheack状态时，会订阅那三个状态，此时的三个状态都会更新。
            所以，我只需要把死亡条件放到任意一个事件响应函数中，就可以完美解决！
         */
        if (hide && clean)
        {
            SubTitle.GetInstance().BirdDorTalk("Do a good job");
            birdDor.StartCoroutine(waitChangeState(birdDor.idleState));

        }
        else
        {
            SubTitle.GetInstance().BirdDorTalk("Patient breached rules");
            kill = true;
        }

    }


    private void isRoomClean(bool _clean)
    {
        clean = _clean;
        /*
         * 在Enter到Cheack状态时，会订阅那三个状态，此时的三个状态都会更新。
            所以，我只需要把死亡条件放到任意一个事件响应函数中，就可以完美解决！
         */
        //if (hide && clean)
        //{
        //    SubTitle.GetInstance().BirdDorTalk("你做的很好");
        //}
        //else
        //{
        //    SubTitle.GetInstance().BirdDorTalk("违反病人守则");
        //    kill = true;
        //}

    }

    IEnumerator waitChangeState(EnemyState targetState)
    {
        yield return new WaitForSeconds(3f);

        birdDor.ChangeState(targetState);
    }


    IEnumerator waitForKill()
    {
        yield return new WaitForSeconds(2f);
    }

    public void KillPlayer()
    {
        float distance = Vector3.Distance(birdDor.transform.position, PlayerMove.instacnce.transform.position);
        birdDor.agent.SetDestination(PlayerMove.instacnce.transform.position);
        birdDor.agent.speed = 8f;
        if (distance < 3)
        {
            birdDor.GetComponent<Rigidbody>().velocity = Vector3.zero;
            birdDor.agent.isStopped = true;
            birdDor.anim.SetBool("kill", true);
            //GameRoot.GetInstacne().DieControl();
            birdDor.StartCoroutine(WaitForKillAnimation());
        }
    }

    IEnumerator WaitForKillAnimation()
    {
        // 获取当前动画长度
        AnimatorStateInfo stateInfo = birdDor.anim.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        // 等待动画时长 + 0.1秒缓冲
        yield return new WaitForSeconds(animationLength + 0.3f);

        // 执行后续逻辑
        GameRoot.GetInstacne().DieControl();

        // 重置动画状态（可选）
        birdDor.anim.SetBool("kill", false);
 
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDor_Check :EnemyState
{
    private BirdDor birdDor;
    public GameState eatState = GameState.isEatMedicine;
    public GameState hideState = GameState.isMedicineDestory;
    public GameState cleanState = GameState.isRoomClean;

    bool eatMedice;
    bool clean;
    bool hide;

    public BirdDor_Check(BirdDor _birdDor)
    {
        birdDor = _birdDor;
    }


    public void Enter()
    {
        StateDetector.Instance.SubscribeToState(eatState, isPlayerEatMedicine);
        StateDetector.Instance.SubscribeToState(hideState,isPlayerHideMedicine);
        StateDetector.Instance.SubscribeToState(cleanState, isRoomClean);
    }

    public void Update()
    {
        if (StateDetector.Instance.GetState(hideState) == true && StateDetector.Instance.GetState(cleanState) == true)
        {
            Debug.Log("检查通过，你做的很好");
            birdDor.StartCoroutine(waitChangeState(birdDor.idleState));
            //birdDor.ChangeState(birdDor.idleState);
        }
        
        float distance = Vector3.Distance(birdDor.transform.position, PlayerMove.instacnce.transform.position);
        if (distance > 4)
        {
            birdDor.StartCoroutine(waitChangeState(birdDor.findPlayerState));
        }


      

    }

    public void Exit()
    {
        StateDetector.Instance.UnsubscribeFromState(eatState, isPlayerEatMedicine);
        StateDetector.Instance.UnsubscribeFromState(hideState, isPlayerHideMedicine);
        StateDetector.Instance.UnsubscribeFromState(cleanState, isRoomClean);
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
    }


    private void isRoomClean(bool _clean)
    {
        clean = _clean;
        /*
         * 在Enter到Cheack状态时，会订阅那三个状态，此时的三个状态都会更新。
            所以，我只需要把死亡条件放到任意一个事件响应函数中，就可以完美解决！
         */
        if (hide && clean)
        {
            GameRoot.GetInstacne().SubtitleControl_BirDor("你做得很好！");
        }
        else
        {

            GameRoot.GetInstacne().SubtitleControl_BirDor("违反病人守则！");
            birdDor.StartCoroutine(waitForDie());

        }

    }

    IEnumerator waitChangeState(EnemyState targetState)
    {
        yield return new WaitForSeconds(3f);

        birdDor.ChangeState(targetState);
    }


    IEnumerator waitForDie()
    {
        yield return new WaitForSeconds(5f);
        GameRoot.GetInstacne().DieControl();

    }


}

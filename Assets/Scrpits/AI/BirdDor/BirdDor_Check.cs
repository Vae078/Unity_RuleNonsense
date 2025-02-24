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
            Debug.Log("���ͨ���������ĺܺ�");
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

    //��Щ���¼���Ӧ����
    private void isPlayerEatMedicine(bool _EatMedicine)  //��ҳ�ҩ
    {
        eatMedice = _EatMedicine;
    }
    
    private void isPlayerHideMedicine(bool _hide)    //��Ҵݻ���ҩ������������ˣ�
    {
        hide = _hide;
    }


    private void isRoomClean(bool _clean)
    {
        clean = _clean;
        /*
         * ��Enter��Cheack״̬ʱ���ᶩ��������״̬����ʱ������״̬������¡�
            ���ԣ���ֻ��Ҫ�����������ŵ�����һ���¼���Ӧ�����У��Ϳ������������
         */
        if (hide && clean)
        {
            GameRoot.GetInstacne().SubtitleControl_BirDor("�����úܺã�");
        }
        else
        {

            GameRoot.GetInstacne().SubtitleControl_BirDor("Υ����������");
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

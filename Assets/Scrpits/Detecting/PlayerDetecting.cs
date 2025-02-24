using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecting : MonoBehaviour
{
    public GameState eatState = GameState.isEatMedicine;
    private float Timer;
    private float TimeToDie = 30f;
    private bool EatMedicine;
    private bool timeStarted;

    private void Start()
    {
        StateDetector.Instance.SubscribeToState(eatState, isEatMedicine);

    }

    private void Update()
    {
        DieTimer();
    }


    //用这个事件响应函数，避免了在Update一直查询状态，减少内存开支！
    private void isEatMedicine(bool _EatMedicine)
    {
        if (_EatMedicine)
        {
            EatMedicine = true;
        }
    }

    private void DieTimer()
    {
        if (EatMedicine && !timeStarted)
        {
            timeStarted = true;
            Timer = TimeToDie;
        }
        if (timeStarted)
        {
            Timer -= Time.deltaTime;
            if (Timer < 0)
            {
                timeStarted = false;
                EatMedicine = false;
                GameRoot.GetInstacne().DieControl();
                Debug.Log("你吃了药，你死了");
            }

        }
         
        
    }


}

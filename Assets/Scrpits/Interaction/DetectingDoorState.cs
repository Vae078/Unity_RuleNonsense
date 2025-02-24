using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingDoorState : PlayerInteractionState
{

    private PlayerInteraction playerInteraction;

    public DetectingDoorState(PlayerInteraction interaction)
    {
        playerInteraction = interaction;
    }

    public void Enter()
    {
        if (!playerInteraction.isChangeUI)
            playerInteraction.PrintUI("--F ʹ����--");
    }

    public void Update()
    {
        RaycastHit hit;
        if (playerInteraction.Hit(out hit))
        {
            IDoorController doorController = hit.collider.GetComponent<IDoorController>();
            if (doorController != null && doorController is ControlDoor controlDoor)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (controlDoor.TryOpenDoor(hit.normal))
                    {
                        return;
                    }
                    else
                    {
                        //��������ʱ���Կ��ţ��򴥷�����
                        GameState doorState = GameState.isDoorTouch;
                        StateDetector.Instance.SetState(doorState, true);
                        playerInteraction.PrintUI("---������---");
                    }
                }
            }
        }
        else
        {
            playerInteraction.ChangeState(playerInteraction.interactionIdle);
        }
       

    }


    public void Exit()
    {
        playerInteraction.HideUI();
    }

}

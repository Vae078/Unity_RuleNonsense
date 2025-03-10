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
            playerInteraction.PrintUI("--F USE DOOR--");
    }

    public void Update()
    {
        RaycastHit hit;
        if (playerInteraction.Hit(out hit))
        {
            ControlDoor doorController = hit.collider.GetComponent<ControlDoor>();
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
                        //当门锁的时候尝试开门，则触发警报
                        GameState doorState = GameState.isDoorTouch;
                        StateDetector.Instance.SetState(doorState, true);
                        playerInteraction.PrintUI("---DOOR HAS CLOCKED---");
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

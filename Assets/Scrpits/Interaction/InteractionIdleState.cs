using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIdleState : PlayerInteractionState
{
    private PlayerInteraction playerInteraction;

    public InteractionIdleState(PlayerInteraction interaction)
    {
        playerInteraction = interaction;
    }

    public void Enter()
    {
        playerInteraction.HideUI();
    }

    public void Update()
    {
        DetectingDoor();
        DetectingCup();
    }

    private void DetectingDoor()
    {
        if (playerInteraction.RayDetect(7) == true)   //检测门
        {
            playerInteraction.ChangeState(playerInteraction.detectingDoor);
        }
    }
    private void DetectingCup()      //检测所有可交互类物体
    {
        if (playerInteraction.RayDetect(6) == true)
        {
            playerInteraction.ChangeState(playerInteraction.detectingCup);
        }
    }


    public void Exit()
    {

    }

}

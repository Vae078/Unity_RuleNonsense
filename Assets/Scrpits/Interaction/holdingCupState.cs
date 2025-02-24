using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holdingCupState : PlayerInteractionState
{
    private PlayerInteraction playerInteraction;
    private ControlCup controlCup;



    public holdingCupState(PlayerInteraction interaction)
    {
        playerInteraction = interaction;
    }

    public void SetCup(ControlCup cup)
    {
        controlCup = cup;
    }

    public void Enter()
    {
        string name = controlCup.name;
        playerInteraction.PrintUI($" -F- ╥еоб{name}");
       
    }

    public void Update()
    {
        controlCup.cupTransform.position = controlCup.playerCamera.transform.TransformPoint(controlCup.holdPositionOffest);
        controlCup.cupTransform.rotation = controlCup.initialRotation;

        if (Input.GetKeyDown(KeyCode.F))
        {
            controlCup.putCup();
            playerInteraction.ChangeState(playerInteraction.interactionIdle);
        }
    }

    public void Exit()
    {
        controlCup = null;
        playerInteraction.HideUI();

    }


}

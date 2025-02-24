using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCupState : PlayerInteractionState
{
    private PlayerInteraction playerInteraction;
    private ControlCup controlCup;
    private string name;

    public DetectingCupState(PlayerInteraction interaction)
    {
        playerInteraction = interaction;
    }


    public void Enter()
    {
        RaycastHit hit;
        if (playerInteraction.Hit(out hit))
        {
            name = hit.collider.name;
            playerInteraction.PrintUI($"-F- ¼ñÆð{name}");
            if (name == "Medicine")
            {
                playerInteraction.PrintUI($"-E- ³ÔÏÂ{name}\n -F- ¼ñÆð{name}\n -G- ²ØÆð{name}");
            }
        }
    }

    public void Update()
    {
      
        RaycastHit hit;
        if (playerInteraction.Hit(out hit) && hit.collider.name == name)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                controlCup = hit.collider.GetComponent<ControlCup>();
                controlCup.pickCup();
                var holdingCup = playerInteraction.GetHoldingCupState(controlCup);
                playerInteraction.ChangeState(holdingCup);
            }

            if (name == "Medicine" && Input.GetKeyDown(KeyCode.E))
            {
                //³ÔÒ©-->³ÔÒ©true+Ïú»ÙÒ©true
                GameObject p = hit.collider.gameObject;
                Object.Destroy(p);
                StateDetector.Instance.SetState(GameState.isEatMedicine, true);
                StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
            }

            if (name == "Medicine" && Input.GetKeyDown(KeyCode.G))
            {
                //²ØÆðÒ©
                GameObject p = hit.collider.gameObject;
                Object.Destroy(p);
                StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
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

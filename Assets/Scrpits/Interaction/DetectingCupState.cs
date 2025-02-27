using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCupState : PlayerInteractionState
{
    private PlayerInteraction playerInteraction;
    private ControlCup controlCup;
    private string name;
    private string tag;

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
            tag = hit.collider.tag;
            playerInteraction.PrintUI($"-F- ¼ñÆð{tag}");
            if (tag == "Medicine")
            {
                playerInteraction.PrintUI($"-E- ³ÔÏÂ{tag}\n -F- ¼ñÆð{tag}\n -G- ²ØÆð{tag}");
            }
            else if (tag == "Clue")
            {
                playerInteraction.PrintUI($"-E- ÔÄ¶Á{tag}\n -G-Ê°Æð{tag}");
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

            if (tag == "Medicine" && Input.GetKeyDown(KeyCode.E))
            {
                //³ÔÒ©-->³ÔÒ©true+Ïú»ÙÒ©true
                GameObject p = hit.collider.gameObject;
                Object.Destroy(p);
                StateDetector.Instance.SetState(GameState.isEatMedicine, true);
                StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
            }

            if (tag == "Medicine" && Input.GetKeyDown(KeyCode.G))
            {
                //²ØÆðÒ©
                GameObject p = hit.collider.gameObject;
                Object.Destroy(p);
                StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
            }

            if (tag == "Clue")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameRoot.GetInstacne().ClueWatch();
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    hit.collider.GetComponent<ItemObject>().Trigger();
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

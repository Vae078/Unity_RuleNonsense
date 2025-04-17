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
            if (tag == "Medicine")
            {
                playerInteraction.PrintUI($"-E- 吃  {name}\n -F- 捡起  {name}\n");
            }
            else if (tag == "item")
            {
                playerInteraction.PrintUI($"-E- 使用  {name}\n -G- 放入背包  {name}");
            }
            else if(tag=="lighting")
            {
                playerInteraction.PrintUI($"-E- 使用  {name}\n ");
            }
            else
            {
                playerInteraction.PrintUI($"-F- 捡起  {name}");
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
                //吃药-->吃药true+销毁药true
                GameObject p = hit.collider.gameObject;
                Object.Destroy(p);
                StateDetector.Instance.SetState(GameState.isEatMedicine, true);
                StateDetector.Instance.SetState(GameState.isMedicineDestory, true);
            }

            if (tag == "item")
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

            if (tag == "lighting")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameRoot.GetInstacne().UseLighting();
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

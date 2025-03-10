using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDor_FindPlayer : EnemyState
{
    private BirdDor birdDor;
    bool isArriveDoor;
    float distance;
    Ray ray;

    public BirdDor_FindPlayer(BirdDor _bird, bool _isArriveDoor = false)
    {
        birdDor = _bird;
        isArriveDoor = _isArriveDoor;
    }

    public void Enter()
    {
        birdDor.agent.updateRotation = true;
        
    }

    public void Update()
    {
        Debug.Log("我在findState");

        DoorDetect();
        if (!isArriveDoor)
        {
            birdDor.agent.SetDestination(birdDor.navi_3.transform.position);
        }
        else
        {
            birdDor.agent.SetDestination(birdDor.navi_4.transform.position);
        }

        distance = Vector3.Distance(birdDor.transform.position, birdDor.navi_4.transform.position);
        if (distance < 1)
        {
            birdDor.agent.isStopped = true;
            birdDor.ChangeState(birdDor.checkState);
        }
        else
            birdDor.agent.isStopped = false;
    }

    public void Exit()
    {

    }

    public void DoorDetect()
    {
        ray = new Ray(birdDor.transform.position, birdDor.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            
            if (hit.collider.tag == "Door")
            {
                ControlDoor doorController = hit.collider.GetComponent<ControlDoor>();
                doorController.OpenDoor((success)=>
                { 
                    if (success)
                    {
                        isArriveDoor = true;
                       // SubTitle.GetInstance().BirdDorTalk("我开门了");
                    }
                    else
                    {
                        //SubTitle.GetInstance().BirdDorTalk("我在尝试开门");
                    }
                });
            }
        }
    }


}
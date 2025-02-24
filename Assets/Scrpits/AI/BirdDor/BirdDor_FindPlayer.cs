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
        DoorDetect();
        if (!isArriveDoor)
        {
            birdDor.agent.SetDestination(birdDor.navi_3.transform.position);
        }
        else
        {
            birdDor.agent.SetDestination(PlayerMove.instacnce.transform.position);
        }

        distance = Vector3.Distance(birdDor.transform.position, PlayerMove.instacnce.transform.position);
        if (distance < 4)
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
                

                IDoorController doorController = hit.collider.GetComponent<IDoorController>();
                doorController.OpenDoor();
                isArriveDoor = true;
            }
        }
    }


}
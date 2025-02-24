using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlDoor : MonoBehaviour, IDoorController
{
    private Animator anim;
    public bool isDoorOpen = false;

   

    public Transform doorTransform;
    private void Start()
    {
        anim = GetComponent<Animator>();
        doorTransform = transform;

    }


    //ʵ�ֽӿڵĿ����ŷ���
    public void OpenDoor()
    {
        if (!isDoorOpen)
        {
            anim.SetTrigger("open");
    
            isDoorOpen = true;
        }
    }
    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            anim.SetTrigger("close");

            isDoorOpen = false;
        }
    }

    //�ж��Ƿ�����潻��
    public bool IsFrontInteraction(Vector3 hitNormal)
    {
        float angle = Vector3.Angle(doorTransform.forward, hitNormal);
        return angle < 90f;
    }


    public bool TryOpenDoor(Vector3 hitNormal)
    {
        if (IsFrontInteraction(hitNormal))
        {
            if (isDoorOpen == false)
            {
                OpenDoor();
            }
            else if (isDoorOpen == true)
            {
                CloseDoor();
            }
            return true;
        }
        else
        {
            return false;
        }
    }



    //��һ��Gizmos���ӻ�
    private void OnDrawGizmos()
    {
        doorTransform = transform;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(doorTransform.position, doorTransform.forward * 2f);
    }



}

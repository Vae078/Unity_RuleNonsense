using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlDoor : MonoBehaviour
{
    private Animator anim;
    public bool isDoorOpen = false;
    public Transform doorTransform;
    private Coroutine openDoorCoroutine;

    private void Start()
    {
        anim = GetComponent<Animator>();
        doorTransform = transform;
    }

    public void OpenDoor(System.Action<bool> onComplete)
    {
        if (!isDoorOpen && openDoorCoroutine == null)
        {
            openDoorCoroutine = StartCoroutine(CheckDoorRotation(onComplete));
        }
        else
        {
            onComplete?.Invoke(false);
        }
    }

    private IEnumerator CheckDoorRotation(System.Action<bool> onComplete)
    {
        float closeEuler_y = transform.localEulerAngles.y;
        anim.SetTrigger("open");
        float timeout = 1f;
        float startTime = Time.time;
        bool success = false;
        while (Time.time < startTime + timeout)
        {
            float current_y = transform.localEulerAngles.y;
            if (Mathf.Abs(current_y - closeEuler_y) > 50f)
            {
                success = true;
                break;
            }
            yield return null;  //等待下一帧
        }

        isDoorOpen = success;
        openDoorCoroutine = null;
        onComplete?.Invoke(success);
    }



    public void CloseDoor()
    {
        if (isDoorOpen)
        {
            anim.SetTrigger("close");

            isDoorOpen = false;
        }
    }

    //判断是否从正面交互
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
                OpenDoor((success) =>
                {
                    if (success)
                    {
                    }
                    else
                    {
                        Debug.Log("门卡住了");
                    }
                });
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



    //画一个Gizmos可视化
    private void OnDrawGizmos()
    {
        doorTransform = transform;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(doorTransform.position, doorTransform.forward * 2f);
    }



}

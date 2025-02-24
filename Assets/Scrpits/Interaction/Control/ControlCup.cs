using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCup : MonoBehaviour
{
    public Transform cupTransform;
    public Camera playerCamera;
    public float pickUpDistance = 3f;
    public Vector3 holdPositionOffest = new Vector3(2f, -0.5f, 1.5f); //手持位置偏移

    private Rigidbody cupRigidbody;
    private Collider cupCollider;

    public Quaternion initialRotation;

    private void Start()
    {
        cupTransform = transform;
        cupRigidbody = GetComponent<Rigidbody>();
        cupCollider = GetComponent<Collider>();
        playerCamera = Camera.main;  //自动获取主摄像机
        
    }

    public void pickCup()
    {
        //禁用物理效果
        cupRigidbody.isKinematic = true;
        cupCollider.enabled = false;

        initialRotation = cupTransform.rotation;

    }

  

    public void putCup()
    {
        cupTransform.SetParent(null);

        cupRigidbody.isKinematic = false;
        cupCollider.enabled = true;

        //计算放置位置
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position;
        Vector3 rayDirection = playerCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, pickUpDistance * 2))
        {
            // 在碰撞点前方稍微偏移防止嵌入
            cupTransform.position = hit.point + hit.normal * 0.1f;
        }
        else
        {
            // 没有碰撞时在最大距离位置
            cupTransform.position = rayOrigin + rayDirection * pickUpDistance * 2;
        }
        //添加少许向前的力
        cupRigidbody.AddForce(playerCamera.transform.forward * 2f, ForceMode.Impulse);
    }

}

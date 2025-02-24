using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCup : MonoBehaviour
{
    public Transform cupTransform;
    public Camera playerCamera;
    public float pickUpDistance = 3f;
    public Vector3 holdPositionOffest = new Vector3(2f, -0.5f, 1.5f); //�ֳ�λ��ƫ��

    private Rigidbody cupRigidbody;
    private Collider cupCollider;

    public Quaternion initialRotation;

    private void Start()
    {
        cupTransform = transform;
        cupRigidbody = GetComponent<Rigidbody>();
        cupCollider = GetComponent<Collider>();
        playerCamera = Camera.main;  //�Զ���ȡ�������
        
    }

    public void pickCup()
    {
        //��������Ч��
        cupRigidbody.isKinematic = true;
        cupCollider.enabled = false;

        initialRotation = cupTransform.rotation;

    }

  

    public void putCup()
    {
        cupTransform.SetParent(null);

        cupRigidbody.isKinematic = false;
        cupCollider.enabled = true;

        //�������λ��
        RaycastHit hit;
        Vector3 rayOrigin = playerCamera.transform.position;
        Vector3 rayDirection = playerCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, pickUpDistance * 2))
        {
            // ����ײ��ǰ����΢ƫ�Ʒ�ֹǶ��
            cupTransform.position = hit.point + hit.normal * 0.1f;
        }
        else
        {
            // û����ײʱ��������λ��
            cupTransform.position = rayOrigin + rayDirection * pickUpDistance * 2;
        }
        //���������ǰ����
        cupRigidbody.AddForce(playerCamera.transform.forward * 2f, ForceMode.Impulse);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ContorlIK : MonoBehaviour
{
    public TwoBoneIKConstraint twoBoneIk;
    public static ContorlIK instance;
    [SerializeField] private Transform lightingTransform;
    [SerializeField] private Transform headTransform;
    [SerializeField]private Transform contorlTransform;
    [SerializeField]public Transform objectTranform;
    [SerializeField] private RigBuilder rigBuilder;
    public Camera playerCamera;
    private Vector3 screenOffest = new Vector3(0.2f, 0.1f, 0.5f);


    public static ContorlIK GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameRoot 获得实例失败");
            return instance;
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
        playerCamera = Camera.main;  //自动获取主摄像机
        twoBoneIk = GetComponent<TwoBoneIKConstraint>();
       // Holding();
        
    }
    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (playerCamera == null)
            return;

        Vector3 viewportPos = new Vector3(
            1 - screenOffest.x,
            screenOffest.y,
            screenOffest.z);

        Vector3 worldPos = playerCamera.ViewportToWorldPoint(viewportPos);
        lightingTransform.position = worldPos;
        lightingTransform.rotation = playerCamera.transform.rotation;
    }
    public void Holding()
    {
        Debug.Log("yes im holding");
        twoBoneIk.data.target = contorlTransform;
        rigBuilder.Build();
    }

    public void UnHolding()
    {
        twoBoneIk.data.target = null;
        rigBuilder.Build();

    }




}

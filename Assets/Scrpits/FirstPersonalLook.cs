using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonalLook : MonoBehaviour
{

    public static FirstPersonalLook Instance;

    public float mouseSensitivity = 200f;
    public Transform playerBody;
    public Transform cameraTransfrom;
    public Image sight;   //准星

    private float xRotation = 0;  // 垂直旋转累积值


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerBody = transform;
        cameraTransfrom = GetComponentInChildren<Camera>().transform;
        LockCursor(); 
    }


    private void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //限制视角上下界限

        cameraTransfrom.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void UnlockCursor()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

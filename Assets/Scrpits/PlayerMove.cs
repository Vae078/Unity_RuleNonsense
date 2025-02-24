using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Transform cameraTransorm;
    private Animator animator;

    public bool canMove = true;  //是否允许移动

    public static PlayerMove instacnce;


    private void Awake()
    {
        if (instacnce == null)
        {
            instacnce = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        rb.useGravity = true;
        cameraTransorm = Camera.main.transform;
    }

    private void Update()
    {

        if (!canMove)
        {// 如果不允许移动，将水平和垂直速度置为 0，保持垂直方向的速度不变
            Vector3 zeroVelocity = Vector3.zero;
            zeroVelocity.y = rb.velocity.y;
            rb.velocity = zeroVelocity;
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //检查是否没有输入
        if (Mathf.Abs(horizontal) < Mathf.Epsilon && Mathf.Abs(vertical) < Mathf.Epsilon)
        {
            Vector3 zeroVelocity = Vector3.zero;
            zeroVelocity.y = rb.velocity.y;
            rb.velocity = zeroVelocity;
            animator.SetBool("isWalk", false);
            return;
        }else
        {
            animator.SetBool("isWalk", true);
        }

        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        Vector3 moveDirection = cameraTransorm.forward * moveInput.z + cameraTransorm.right * moveInput.x;
        moveDirection.y = 0;
        moveDirection.Normalize();

        Vector3 velocity = moveDirection * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

    }
}

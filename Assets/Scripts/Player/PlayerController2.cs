using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerController2 : MonoBehaviour
{
    public Vector3 PlayerMovementInput;

    [SerializeField] private Transform cam;
    [SerializeField] CinemachineImpulseSource impulse;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private const int MAX_HEALTH = 100;
    [SerializeField] private int health = 100;

    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";

    private bool isWalking;
    private bool isJumping;
    private bool isFalling;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis(AXIS_HORIZONTAL), 0f, Input.GetAxis(AXIS_VERTICAL));
        MovePlayer();
        PlayerJump();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = PlayerMovementInput * speed;

        // Camera Direction
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Relative to camera
        Vector3 forwardRelative = MoveVector.z * camForward;
        Debug.Log("/////////////////////");
        Debug.Log(camRight);
        Vector3 moveDirection = forwardRelative;
        Debug.Log(moveDirection);

        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, moveDirection.z);
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            isJumping = true;
            rb.velocity += new Vector3(0, jumpForce, 0);
        }
    }
}

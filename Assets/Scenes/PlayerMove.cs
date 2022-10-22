using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultipayer;
    bool readyToJump = true;

    public float wallrunSpeed;

    [Header("Keybinds")]
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisground;
    bool grounded;

    public Transform orientation;

    float hor;
    float vert;

    Vector3 moveDir;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        wallrunning
    }

    public bool wallrunning;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisground);

        MyInput();
        SpeedControll();

        if (grounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void StateHandeler()
    {
        if (wallrunning)
        {
            state = MovementState.wallrunning;

            speed = wallrunSpeed;
        }
    }

    private void MyInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        //jump
        if (Input.GetKey(jumpkey) && grounded && readyToJump)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        StateHandeler();
    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * vert + orientation.right * hor;

        if (grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f, ForceMode.Force);
        } else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * speed * 10f * airMultipayer, ForceMode.Force);
        }
    }

    private void SpeedControll()
    {
        Vector3 vector3 = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (vector3.magnitude > speed)
        {
            Vector3 limitedVel = vector3.normalized * speed;

            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}

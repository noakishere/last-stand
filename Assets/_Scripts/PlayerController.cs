using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster controls;
    private Vector3 velocity;
    private const float gravity = -9.81f;
    private Vector2 move;

    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpHeight = 2.4f;
    
    private CharacterController controller;

    [Header("Ground checks")]
    [SerializeField] private Transform ground;
    [SerializeField] private float distanceToGround = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        controls = new InputMaster();
        controller = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        Gravity();
        PlayerMovement();
        Jump();
    }

    private void Gravity()
    {
        // shoot a short ray straight down from the player's feet
        RaycastHit hit;
        float rayLength = distanceToGround + 0.1f;
        if (Physics.Raycast(ground.position, Vector3.down, out hit, rayLength, groundMask))
        {
            // only consider yourself grounded if that surface is mostly horizontal
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            isGrounded = angle < 60f;     // e.g. only surfaces within 60° of horizontal
        }
        else
        {
            isGrounded = false;
        }

        //if (isGrounded && velocity.y < 0f)
        //{
        //    velocity.y = -2f;  // a small downward tug to stay snapped
        //}

        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        //if(isGrounded && velocity.y < 0)
        //{
        //    velocity.y = -2f;
        //}

        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        move = controls.Player.Movement.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if(isGrounded && controls.Player.Jump.triggered)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

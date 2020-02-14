using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 direction;
    private float velocity = 2.5f;
    private float verticalVelocity;
    private float gravity = 10.0f;
    private float jumpVelocity = 5.0f;

    void Awake()
    {
        this.characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        this.direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0.0f,
            Input.GetAxis("Vertical")
        );

        this.direction = this.transform.TransformDirection(this.direction);
        this.direction *= this.velocity * Time.deltaTime;
        MoveGravity();
        this.characterController.Move(this.direction);
    }

    void MoveGravity()
    {
        if (this.characterController.isGrounded)
        {
            this.verticalVelocity -= this.gravity * Time.deltaTime;
            this.Jump();
        }
        else
        {
            this.verticalVelocity -= this.gravity * Time.deltaTime;
        }

        this.direction.y = this.verticalVelocity * Time.deltaTime;
    }

    void Jump()
    {
        if (this.characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            this.verticalVelocity = this.jumpVelocity;
        }
    }
}

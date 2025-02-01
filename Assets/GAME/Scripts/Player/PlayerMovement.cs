using System.Collections;
using UnityEditor.Media;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private float dashTime;
    private bool isDashing = false;
    private float dashCooldownTimer;

    private Animator animator;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        animator.Play("idle");
    }

    void Update()
    {
        HandleDashInput();
        UpdateDashCooldown();
        UpdateAnimationState();
    }

    void FixedUpdate()
    {
        Move();
        Dash();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Hareket yönüne doðru dönme
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private void Dash()
    {
        if (isDashing)
        {
            rb.linearVelocity = moveDirection * dashSpeed;
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
                rb.linearVelocity = Vector3.zero;
            }
        }
        else
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void UpdateDashCooldown()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void UpdateAnimationState()
    {
        if (isDashing || moveDirection.magnitude > 0)
        {
            animator.Play("run");
        }
        else
        {
            animator.Play("idle");
        }
    }
}



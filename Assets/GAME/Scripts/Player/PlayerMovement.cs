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

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
           
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

 
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
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
    }

    private void Dash()
    {
        if (isDashing)
        {
            // Dash s�ras�nda h�z art�r�l�r
            rb.linearVelocity = moveDirection * dashSpeed;
            dashTime -= Time.fixedDeltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
                rb.linearVelocity = Vector3.zero; // Dash sonras� h�z s�f�rlan�r
            }
        }
        else
        {
            // Normal hareket
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }
}


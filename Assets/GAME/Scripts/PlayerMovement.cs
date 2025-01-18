using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Normal hareket h�z�
    public float dashSpeed = 20f; // Dash s�ras�nda uygulanacak h�z
    public float dashDuration = 0.2f; // Dash s�resi
    public float dashCooldown = 1f; // Dash i�in bekleme s�resi

    private float dashTime; // Dash s�resi takibi
    private bool isDashing = false;
    private float dashCooldownTimer; // Dash cooldown s�resi takibi

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Hareket y�n�n� al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Dash i�lemi ba�lat
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        // Cooldown timer'� g�ncelle
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
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


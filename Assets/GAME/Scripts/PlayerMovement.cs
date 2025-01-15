using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float jumpForce = 5f;
    private bool isGrounded;

    [Header("Dash")]
    public float dashDistance = 5f;
    public float dashCooldown = 1f;
    private bool canDash = true;

   
    private Rigidbody rb;
    private Vector3 lastMoveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {       
        Jump();

    }

    void FixedUpdate()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }


    void MovePlayer()
    {
        // Klavyeden giriþ al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hareket vektörünü hesapla
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Hareket et
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Oyuncunun yönünü hareket yönüne doðru döndür
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // Havadayken tekrar zýplamayý engelle
        }
    }

    IEnumerator Dash()
    {
        canDash = false;

        // Dash yönünü belirle
        Vector3 dashDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dashDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dashDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dashDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dashDirection = Vector3.right;
        }

        dashDirection = transform.TransformDirection(dashDirection.normalized);

        // Dash hareketini yap
        Vector3 dashPosition = transform.position + dashDirection * dashDistance;
        rb.MovePosition(dashPosition);

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}

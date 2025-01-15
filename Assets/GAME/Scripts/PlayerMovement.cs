using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Klavyeden giri� al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hareket vekt�r�n� hesapla
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Hareket et
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Oyuncunun y�n�n� hareket y�n�ne do�ru d�nd�r
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}

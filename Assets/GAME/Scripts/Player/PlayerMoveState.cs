using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float moveSpeed = 5f;

    public PlayerMoveState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator) { }

    public override void Enter()
    {
        animator.Play("run");
    }

    public override void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateMachine.ChangeState(new PlayerIdleState(stateMachine, rb, animator));

        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.CanDash())
            stateMachine.ChangeState(new PlayerDashState(stateMachine, rb, animator));
    }

    public override void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        if (moveDirection.magnitude > 0)
        {
            // 🎯 Karakterin yönünü hareket yönüne çevir
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f));

            // 🎯 Hareket ettir
            Vector3 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            stateMachine.ChangeState(new PlayerIdleState(stateMachine, rb, animator));
        }
    }

    public override void Exit() { }
}

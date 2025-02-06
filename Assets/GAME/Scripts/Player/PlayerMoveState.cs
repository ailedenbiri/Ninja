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
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;
        if (moveDirection.magnitude > 0.1f)
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(moveDirection), Time.fixedDeltaTime * 10f);
    }

    public override void Exit() { }
}

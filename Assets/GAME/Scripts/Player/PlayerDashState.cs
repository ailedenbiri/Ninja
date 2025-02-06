using UnityEngine;

public class PlayerDashState : PlayerState
{

    private float dashSpeed = 20f;
    private float dashDuration = 0.2f;
    private float dashTime;

    public PlayerDashState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator) { }

    public override void Enter()
    {
        animator.Play("dash");
        dashTime = dashDuration;
        stateMachine.StartDashCooldown();
    }

    public override void Update()
    {
        dashTime -= Time.deltaTime;
        if (dashTime <= 0)
            stateMachine.ChangeState(new PlayerIdleState(stateMachine, rb, animator));
    }

    public override void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        rb.linearVelocity = moveDirection * dashSpeed;
    }

    public override void Exit() { }
}

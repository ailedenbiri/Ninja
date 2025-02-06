using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
        : base(stateMachine, rb, animator) { }

    public override void Enter()
    {
        animator.Play("idle");
    }

    public override void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            stateMachine.ChangeState(new PlayerMoveState(stateMachine, rb, animator));

        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.CanDash())
            stateMachine.ChangeState(new PlayerDashState(stateMachine, rb, animator));
    }

    public override void FixedUpdate() { }
    public override void Exit() { }
}


using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Rigidbody rb;
    protected Animator animator;

    public PlayerState(PlayerStateMachine stateMachine, Rigidbody rb, Animator animator)
    {
        this.stateMachine = stateMachine;
        this.rb = rb;
        this.animator = animator;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}


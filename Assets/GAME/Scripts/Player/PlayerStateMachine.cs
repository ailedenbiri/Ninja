using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState currentState;
    private Rigidbody rb;
    private Animator animator;
    private float dashCooldown = 1f;
    private float dashCooldownTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        ChangeState(new PlayerIdleState(this, rb, animator));
    }

    void Update()
    {
        currentState.Update();
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool CanDash() => dashCooldownTimer <= 0;
    public void StartDashCooldown() => dashCooldownTimer = dashCooldown;
}



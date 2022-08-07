using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerInput), typeof(Shooter))]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private PlayerInput input;

    private Shooter shooter;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    [SerializeField] private float shootingStateDelay;

    private bool isShooting = false;

    public bool IsLookingRight
    {
        get
        {
            return !spriteRenderer.flipX;
        }
        private set { }
    }

    private AnimatorState State
    {
        get { return (AnimatorState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        if (!playerMovement.IsJumping && !isShooting)
        {
            if (playerMovement.IsGrounded) State = AnimatorState.idle;

            if (Mathf.Abs(input.HorizontalDirection) > 0.05f && playerMovement.IsGrounded)
            {
                State = AnimatorState.move;
                spriteRenderer.flipX = input.HorizontalDirection < 0.0f;
            }
        }


        if (playerMovement.IsJumping)
        {
            State = AnimatorState.jump;
        }

    }

    public void PlayShootAnimation()
    {

        State = AnimatorState.shoot;
        StartCoroutine(TurnOffShootingState());
        isShooting = true;

    }

    private IEnumerator TurnOffShootingState()
    {
        yield return new WaitForSeconds(shootingStateDelay);
        isShooting = false;
    }
}

public enum AnimatorState
{
    idle,
    move,
    jump,
    shoot
}

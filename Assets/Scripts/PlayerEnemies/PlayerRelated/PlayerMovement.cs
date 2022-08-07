using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [SerializeField] private float jumpForce = 15f;

    [SerializeField] private int maxJumps = 2;

    [SerializeField] private float externalImpactForce;

    private bool canDoubleJump = true;

    private Rigidbody2D rigidbody;

    private PlayerInput input;

    public bool IsGrounded { get; private set; } = false;

    public bool IsJumping { get; private set; } = false;

    [SerializeField] private float groundCheckRadius = 0.3f;

    [SerializeField] private LayerMask groundMask;

    [SerializeField]
    private float horizontalInputTreshold = 0.1f;

    [SerializeField] private float blockMovementOnExternalImpactSeconds;

    [SerializeField] private AnimationCurve speedCurve;

    private bool canMove = true;

    public static Action onPlayerJumpDown;

    private void OnEnable()
    {
        GameManager.onFail += StopMoving;
        GameManager.onWin += StopMoving;

    }

    private void OnDisable()
    {
        GameManager.onFail -= StopMoving;
        GameManager.onWin -= StopMoving;
    }

    private void StopMoving()
    {
        rigidbody.velocity = Vector2.zero;
        canMove = false;
    }


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Mathf.Abs(input.HorizontalDirection) > horizontalInputTreshold && canMove)
        {
            MoveToDirection(input.HorizontalDirection);
        }

        if (input.isJumpButtonPressed)
        {
            if (IsGrounded)
            {
                canDoubleJump = true;
                Jump();
                IsJumping = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        if (input.isDownButtonPressed)
        {
            if (IsGrounded)
            {
                JumpDown();
            }
        }

        if (rigidbody.velocity.y <= 0.1f) IsJumping = false;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void MoveToDirection(float direction)
    {
        rigidbody.velocity = new Vector2(speedCurve.Evaluate(direction), rigidbody.velocity.y);
    }

    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
    }
    
    private void JumpDown()
    {
        onPlayerJumpDown?.Invoke();
        
        print("Jump down called");
    }

    private void CheckGround()
    {
        IsGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundMask);
    }

    public void ApplyExternalImpact(Vector2 fromPosition)
    {
        StartCoroutine(BlockMovementOnSeconds(blockMovementOnExternalImpactSeconds));
        rigidbody.velocity = Vector2.zero;
        float horizontalDirection = Mathf.Sign(transform.position.x - fromPosition.x);
        rigidbody.velocity = new Vector2(externalImpactForce * horizontalDirection, externalImpactForce / 1.5f);
    }

    private IEnumerator BlockMovementOnSeconds(float seconds)
    {
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}



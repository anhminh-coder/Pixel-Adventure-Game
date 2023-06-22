using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private bool isFacingRight = true;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    private int extraJump;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum MovementState { idle, running, jumping, falling, doubleJumping, wallSliding};
    [SerializeField] private AudioSource jumpSoundEffect;
    MovementState state;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck2;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private bool canWallSlide;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f; 
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            dirX = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirX = 1f;
        }
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (IsGrounded())
        {
            extraJump = 1;
        }
        if (Input.GetButtonDown("Jump2") && IsGrounded()) 
        {
            jumpSoundEffect.Play();
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonDown("Jump2") && extraJump > 0) {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJump--;
        }

        Flip();
        UpdateAnimationState(); 
        WallSlide();
        WallJump();
    }

    private void Flip()
    {
        if ((isFacingRight && dirX < 0f) || (!isFacingRight && dirX > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate() {
        if (!isWallJumping) {
            rb.velocity = new Vector2(dirX * 8f, rb.velocity.y);
        }
    }
    private void UpdateAnimationState()
    {

        if (dirX > 0f) 
        {
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else 
        {
            state = MovementState.idle;
        }
    
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            if (Input.GetButtonDown("Jump2")) {
                state = MovementState.doubleJumping;
            }
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        
        if (isWallSliding) 
        {
            state = MovementState.wallSliding;
        }
        
        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        // return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);
    }

    private bool IsWalled2()
    {
        return Physics2D.Raycast(wallCheck2.position, Vector2.right, wallCheckDistance, wallLayer);
    }
    private void WallSlide()
    {
        if ((IsWalled() || IsWalled2()) && !IsGrounded() && dirX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection =  -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else 
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump2") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection + wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        Invoke(nameof(StopWallJumping), wallJumpingDirection);
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, 
                                                                wallCheck.position.y,
                                                                wallCheck.position.z));
        Gizmos.DrawLine(wallCheck2.position, new Vector3(wallCheck2.position.x + wallCheckDistance, 
                                                                wallCheck2.position.y,
                                                                wallCheck2.position.z));

    }
}

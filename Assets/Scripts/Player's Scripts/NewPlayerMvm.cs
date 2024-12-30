using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMvm : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private bool reverseGravity;

    private float dirX;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableCeiling;
    [SerializeField] private AudioSource jumpSound;
    

    private enum MovementState { idle, running, jumping, falling}
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (IsGrounded())
        {
            Jump();
        }
        else if (OnCeilingJump())
        {
            CeilingJump();
        }

        UpdateAnimationState();
    }  

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }
    }

    private void CeilingJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            jumpSound.Play();
        }
 
    }
    private bool IsGrounded()
    {
        Vector2 direction = reverseGravity ? Vector2.up : Vector2.down;
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool OnCeilingJump()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, jumpableCeiling);
    }

    private void ReverseGravity()
    {
        reverseGravity = !reverseGravity;
        rb.gravityScale *= -1f;
        transform.Rotate(180, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GravityReverser")) // Ensure this tag is set on the special object
        {
            ReverseGravity();
        } 
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("GravityReverser"))
        {
            ReverseGravity();
        }
    }
}

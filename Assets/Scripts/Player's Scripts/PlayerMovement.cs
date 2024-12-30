using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]private float jumpforce = 14f;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;


    private enum MovementState { idle, running,jumping,falling}

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;
    private bool facingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        MovePlayer();
    }

    void Update()
    {
        MovementState state;
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.08f, 1.4f), 0, wallLayer);

        MovePlayer();

        if (isWallTouch)
        {
            Flip();
        }

        if (speed > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(speed < 0f)
        {
            state =MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);
    }

    private void MovePlayer()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;

        // Flip the player's sprite by scaling it negatively on the x-axis
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float sprint = 4f;
    private enum MovementState { idle, running, jumping, falling, doublejump}
    private bool isSprinting = false;
    private bool isGrounded = false;
    private int DoubleJump = 0;

    [SerializeField] private AudioSource JumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * (isSprinting ? moveSpeed + sprint : moveSpeed), rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            while (DoubleJump < 3)
            {
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                DoubleJump++;
            }

        }

            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                isSprinting = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isSprinting = false;
            }


        UpdateAnimationState();

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            DoubleJump = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            DoubleJump = 0;
        }
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
            if (DoubleJump > 2 && !isGrounded)
            {
                state = MovementState.doublejump;
            }
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
}

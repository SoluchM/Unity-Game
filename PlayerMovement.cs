using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float sprint = 4f;
    private enum MovementState { idle, running, jumping, falling, doublejump, wallstick}
    private bool isSprinting = false;
    private bool isGrounded = false;
    private int DoubleJump = 0;
    private bool WallStick = false;
    private GameObject playerObj = null;


    [SerializeField] private AudioSource JumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (playerObj == null) playerObj = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * (isSprinting ? moveSpeed + sprint : moveSpeed), rb.velocity.y);

        if (Input.GetButtonDown("Jump")) {

            if (WallStick)
            {
                rb.gravityScale = 2;
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce - 2f);
                WallStick = false;
            }
            else
            {
                while (DoubleJump < 3)
                {
                    JumpSoundEffect.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    DoubleJump++;
                }

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
            WallStick= false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(playerObj.transform.position.x, 0);
            rb.gravityScale = 0;
            DoubleJump = 0;
            WallStick = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            DoubleJump = 0;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            WallStick = false;
            rb.gravityScale = 2;
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

        if (WallStick == false && !isGrounded && rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            if (DoubleJump > 2 && !isGrounded)
            {
                state = MovementState.doublejump;
            }
        }
        else if (rb.velocity.y < -.1f && WallStick == false)
        {
            state = MovementState.falling;
        }
        if(WallStick == true)
        {
            state = MovementState.wallstick;
        }

        anim.SetInteger("state", (int)state);
    }
}

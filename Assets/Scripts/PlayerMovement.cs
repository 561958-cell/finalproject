using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool jumpPressed;

    private bool isGrounded;

    [HideInInspector]
    public bool isReplayGhost = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ONLY real player reads keyboard input
        if (!isReplayGhost)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                jumpPressed = true;
            }
        }
    }

    void FixedUpdate()
    {
        CheckGround();

        Move();

        Jump();

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", isGrounded);
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Jump()
    {
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );
        }

        jumpPressed = false;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }

    // THIS is what replay system will use
    public void SetReplayInput(float horizontal, bool jump)
    {
        moveInput = horizontal;

        if (jump)
        {
            jumpPressed = true;
        }
    }
}
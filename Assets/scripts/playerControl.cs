using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of the player's movement
    public float jumpForce = 5f;        // Force applied when jumping
    public LayerMask groundLayer;       // Layer representing the ground
    public Transform groundCheck;       // Transform used to check if the player is on the ground
    public float groundCheckRadius = 0.2f; // Radius of the ground check

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle player movement and actions
        Move();
        Jump();
        PauseGame();
    }

    void Move()
    {
        float moveInput = 0f;

        // Check for left and right input
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f; // Move left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f; // Move right
        }

        // Apply the movement velocity
        Vector2 moveVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused; // Toggle pause state

            if (isPaused)
            {
                Time.timeScale = 0f; // Pause the game
                Debug.Log("Game Paused");
            }
            else
            {
                Time.timeScale = 1f; // Resume the game
                Debug.Log("Game Resumed");
            }
        }
    }
}
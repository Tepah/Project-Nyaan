using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAppearence : MonoBehaviour
{
    public GameObject player;
    public ScoreManager scoreManager;
    public float followSpeed = 2.5f;
    public float jumpForce = 5f;
    public float jumpCoolDown = 2f; // Reduced to 2 seconds

    private float jumpTimer;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    void Start()
    {
        jumpTimer = jumpCoolDown;

        if (player == null)
        {
            Debug.LogError("Player not assigned in BossAppearence.cs");
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("RigidBody2D component is missing");
        }
    }

    void Update()
    {
        FollowPlayer();
        JumpInterval(); // Automatically jump every 2 seconds
    }

    void FixedUpdate()
    {
        // Restore gravity scale when falling
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 2f; // Faster descent
        }
    }

    void FollowPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 bossPosition = transform.position;

        Vector3 direction = (playerPosition - bossPosition).normalized;

        rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

        FlipBoss(direction.x);
    }

    void JumpInterval()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f)
        {
            jumpTimer = jumpCoolDown; // Reset the timer

            if (isGrounded())
            {
                rb.AddForce(Vector2.up * (jumpForce * 1.5f), ForceMode2D.Impulse); // Jump with lower force
                rb.gravityScale = 0.5f; // Slow upward movement
                Debug.Log("Boss jumped!");
            }
            else
            {
                Debug.Log("Boss not grounded, skipping jump.");
            }
        }
    }

    bool isGrounded()
    {
        float rayLength = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);

        Debug.DrawRay(groundCheck.position, Vector2.down * rayLength, Color.red);

        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Boss touched player! Game Over");
        PlayerPrefs.SetInt("score", scoreManager.score);
        scoreManager.UpdateHighScores();
        SceneManager.LoadScene("GameOver");
    }

    void FlipBoss(float directionX)
    {
        if (directionX > 0 && !isFacingRight)
        {
            isFacingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (directionX < 0 && isFacingRight)
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}

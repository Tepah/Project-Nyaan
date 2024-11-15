using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossAppearence : MonoBehaviour
{
    public GameObject player;
    public ScoreManager scoreManager;
    public float followSpeed = 2f;
    public float jumpForce = 5f;
    public float jumpCoolDown = 3f;

    private float jumpTimer;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    
    void Start()
    {   
        // AcitvateBoss();
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
        JumpTowardsPlayer();
    }

    void FollowPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 bossPosition = transform.position;

        Vector3 direction = (playerPosition - bossPosition).normalized;

        rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

        FlipBoss(direction.x);
    }

    void JumpTowardsPlayer()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f && isGrounded())
        {
            jumpTimer = jumpCoolDown;
            Vector2 jumpDirection = (player.transform.position - transform.position).normalized;
            rb.AddForce(new Vector2(jumpDirection.x * jumpForce, jumpForce), ForceMode2D.Impulse);

            FlipBoss(jumpDirection.x);
        }
    }

    bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
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
        Debug.Log("Boss touched player! Gamer Over");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearence : MonoBehaviour
{
    public int scoreThreshold = 0;
    public GameObject bossSprite;
    public GameObject playerSprite;   // Reference to the player sprite
    public float followSpeed = 2f;
    public float jumpSpeed = 5f;
    public float jumpCoolDown = 3f;

    private float jumpTimer;
    private int playerScore = 0;
    private bool bossActive = false;
    private ScoreManager scoreManager;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        bossSprite.SetActive(false);
        jumpTimer = jumpCoolDown;
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    void Update()
    {
        if (!bossActive && playerScore >= scoreThreshold)
        {
            ActivateBoss();
        }

        if (bossActive)
        {
            FollowPlayer();
            JumpTowardsPlayer();
        }
    }

    void ActivateBoss()
    {
        bossSprite.SetActive(true);
        bossActive = true;
    }

    void FollowPlayer()
    {
        Vector3 playerPosition = playerSprite.transform.position;  // Access player position
        Vector3 bossPosition = bossSprite.transform.position;

        Vector3 direction = (playerPosition - bossPosition).normalized;
        bossSprite.transform.position = Vector3.MoveTowards(bossPosition, playerPosition, followSpeed * Time.deltaTime);
    }

    void JumpTowardsPlayer()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f)
        {
            jumpTimer = jumpCoolDown;
            Vector3 jumpDirection = (playerSprite.transform.position - bossSprite.transform.position).normalized;
            bossSprite.transform.position += jumpDirection * jumpSpeed;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void UpdateScore(int score)
    {
        playerScore = score;
    }
}

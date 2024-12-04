using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 9f;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [Header("Events")]
	[Space]

    public UnityEvent OnLandEvent;
    public class BoolEvent : UnityEvent<bool> { }



    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            animator.SetBool("isJumping", true);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        }

        Flip();
        quitGame();
    }

    public void Awake()
    {
        if(OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    public void onLanding()
    {
        animator.SetBool("isJumping", false);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isGrounded() && animator.GetBool("isJumping"))
        {
            onLanding();
        }
    }

    private bool isGrounded() // Change void to bool
    {
        // Return true if the groundCheck overlaps with the groundLayer
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void quitGame()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
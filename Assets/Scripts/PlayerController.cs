using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Variables for movement
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    // References to components
    private Rigidbody2D rb;
    private Collider2D coll;

    // Layer for ground detection
    public LayerMask groundLayer;
    private Animator animator;
    private bool isDead = false;
    private GameManager gameManager;

    void Start()
    {
        // Get the components
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        // Check if the player is dead
        if (isDead)
        {
            // Stop all movement and input
            return;
        }

        // Get input for movement
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            animator.SetBool("IsRun", true);
            // Face right
            if (moveInput > 0)
                transform.localScale = new Vector3(0.2280096f, 0.2280096f, 0.2280096f);
            // Face left
            else
                transform.localScale = new Vector3(-0.2280096f, 0.2280096f, 0.2280096f);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        // Check if the player is on the ground
        isGrounded = Physics2D.IsTouchingLayers(coll, groundLayer);

        // Debugging: Check if the ground detection is working
        Debug.Log("Is Grounded: " + isGrounded);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Debugging: Check if the jump is triggered
            Debug.Log("Jump triggered");
        }

        // Kembalikan animasi ke Idle setelah melompat
        if (isGrounded && !Input.GetButton("Jump"))
        {
            animator.SetBool("IsJump", false);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            // Cek apakah pemain melompat di atas musuh
            if (rb.velocity.y < 0)
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.EnemyIsAttcked();
                }
                // Hancurkan musuh
                // Destroy(collider.gameObject);
                // Beri pemain lompatan ekstra
                rb.velocity = new Vector2(rb.velocity.x, 2);

                // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                PlayerIsAttacked();
                // Implementasi logika ketika pemain terkena musuh
                Debug.Log("dari script player, Player hit by enemy!");
                // Misalnya, kurangi health pemain atau restart level
            }
        }

        if (collider.gameObject.CompareTag("Trap"))
        {
            PlayerIsAttacked();
        }
    }

    public bool IsMovingUpwards()
    {
        return rb.velocity.y > 0;
    }

    public void PlayerIsAttacked()
    {
        animator.SetBool("IsDead", true);
        isDead = true;
        gameManager.IsKalah();
        // Invoke("RestartScene", 1f);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
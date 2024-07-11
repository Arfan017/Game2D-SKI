using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for movement
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    // References to components
    private Rigidbody2D rb;
    private Collider2D coll;

    // Layer for ground detection
    public LayerMask groundLayer;

    void Start()
    {
        // Get the components
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Get input for movement
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if the player is on the ground
        isGrounded = Physics2D.IsTouchingLayers(coll, groundLayer);

        // Debugging: Check if the ground detection is working
        Debug.Log("Is Grounded: " + isGrounded);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Debugging: Check if the jump is triggered
            Debug.Log("Jump triggered");
        }
    }
}

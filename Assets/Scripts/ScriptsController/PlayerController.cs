using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float maxMoveSpeed = 5f;
    public float acceleration = 50f;
    public float deceleration = 50f;
    public float jumpHeight = 4f;
    public AudioSource audioJump;
    public AudioSource audioHitEnemy;
    private float jumpForce;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Collider2D coll;
    public LayerMask groundLayer;
    private Animator animator;
    private bool isDead = false;
    private GameManager gameManager;

    // UI Buttons
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;

    private float moveInput = 0f;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;

        SetupButton(leftButton, -1);
        SetupButton(rightButton, 1);
        SetupJumpButton();

        // Calculate jump force based on desired jump height
        jumpForce = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
    }

    void SetupButton(Button button, float direction)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) => { moveInput = direction; });
        trigger.triggers.Add(pointerDown);

        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((data) => { moveInput = 0f; });
        trigger.triggers.Add(pointerUp);
    }

    void SetupJumpButton()
    {
        EventTrigger trigger = jumpButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((data) => { Jump(); });
        trigger.triggers.Add(pointerDown);
    }

    void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.IsTouchingLayers(coll, groundLayer);

        // Smooth movement
        if (moveInput != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxMoveSpeed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.deltaTime);
        }

        // Apply movement
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // Animation and sprite flipping
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            animator.SetBool("IsRun", true);
            transform.localScale = new Vector3(Mathf.Sign(currentSpeed) * 0.2280096f, 0.2280096f, 0.2280096f);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        // Reset jump animation
        if (isGrounded && rb.velocity.y <= 0)
        {
            animator.SetBool("IsJump", false);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            audioJump.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJump", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && !isDead)
        {
            if (rb.velocity.y < 0)
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.EnemyIsAttcked();
                }
                audioHitEnemy.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2); // Bounce after hitting enemy
            }
            else
            {
                PlayerIsAttacked();
                Debug.Log("dari script player, Player hit by enemy!");
            }
        }

        if (collider.gameObject.CompareTag("Trap") && !isDead)
        {
            PlayerIsAttacked();
            Debug.Log("dari script player, Player hit by trap!");
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
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
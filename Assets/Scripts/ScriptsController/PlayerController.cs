using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

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
    private Vector3 checkpointPosition;
    private bool isRespawning = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;

        SetupButton(leftButton, -1);
        SetupButton(rightButton, 1);
        SetupJumpButton();

        // Calculate jump force based on desired jump height
        jumpForce = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
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

        // Kontrol keyboard
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        if (keyboardInput != 0)
        {
            moveInput = keyboardInput;
        }
        else if (moveInput != 0 && !Input.GetMouseButton(0)) // Jika tidak ada input keyboard dan tidak ada touch input
        {
            moveInput = 0;
        }

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

        // Keyboard jump
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
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
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Snake") && !isDead && !isRespawning)
        {
            if (rb.velocity.y < 0)
            {
                EnemyController enemyController = collider.GetComponent<EnemyController>();
                SnakeEnemy snakeEnemy = collider.GetComponent<SnakeEnemy>();

                if (enemyController != null)
                {
                    enemyController.EnemyIsAttcked();
                }
                else if (snakeEnemy != null)
                {
                    snakeEnemy.EnemyIsAttcked();
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

        if (!isDead && !isRespawning)
        {
            isDead = true;
            animator.SetBool("IsDead", true);
            gameManager.IsKalah();

            if (gameManager.IsRespawn)
            {
                StartCoroutine(RespawnWithDelay(2f));
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void CheckCollisions()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && !isDead)
            {
                HandleEnemyCollision(hitCollider);
            }
            else if (hitCollider.CompareTag("Snake") && !isDead)
            {
                HandleEnemySnakeCollision(hitCollider);
            }
            else if (hitCollider.CompareTag("Trap") && !isDead)
            {
                PlayerIsAttacked();
                Debug.Log("dari script player, Player hit by trap!");
            }
        }
    }

    void HandleEnemyCollision(Collider2D enemyCollider)
    {
        if (rb.velocity.y < 0)
        {
            EnemyController enemyController = enemyCollider.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.EnemyIsAttcked();
            }

            audioHitEnemy.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
        }
        else
        {
            PlayerIsAttacked();
            Debug.Log("dari script player, Player hit by enemy!");
        }
    }
    
    void HandleEnemySnakeCollision(Collider2D enemyCollider)
    {
        if (rb.velocity.y < 0)
        {
            SnakeEnemy snakeEnemy = enemyCollider.GetComponent<SnakeEnemy>();

            if (snakeEnemy != null)
            {
                snakeEnemy.EnemyIsAttcked();
            }

            audioHitEnemy.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
        }
        else
        {
            PlayerIsAttacked();
            Debug.Log("dari script player, Player hit by enemy!");
        }
    }
    void RespawnPlayer()
    {
        transform.position = checkpointPosition;
        rb.velocity = Vector2.zero;
        isDead = false;
        animator.SetBool("IsDead", false);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
        Debug.Log("Checkpoint set at: " + position);
    }

    private IEnumerator RespawnWithDelay(float delay)
    {
        isRespawning = true;
        yield return new WaitForSeconds(delay);
        RespawnPlayer();
        isRespawning = false;
    }
}
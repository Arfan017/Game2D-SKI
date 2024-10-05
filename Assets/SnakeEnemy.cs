using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public float attackCooldown = 2f; // Waktu antara serangan
    private Animator animator;
    private Transform player;
    private bool isFacingRight = false; // Snake mulai menghadap ke kiri
    private float lastAttackTime;
    public Collider2D CollSnakeAttack;

    void Start()
    {
        CollSnakeAttack.enabled = false;
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the snake!");
        }

        // Pastikan snake menghadap ke kiri saat mulai
        Flip();
        lastAttackTime = -attackCooldown; // Memungkinkan serangan instan pertama kali
    }

    void Update()
    {
        if (player != null)
        {
            UpdateFacingDirection();
            TryAttack();
        }
    }

    void UpdateFacingDirection()
    {
        bool shouldFaceRight = player.position.x > transform.position.x;
        if (shouldFaceRight != isFacingRight)
        {
            Flip();
        }
    }

    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Attack()
    {
        Debug.Log("Snake attacking!");
        animator.SetTrigger("Attack");
    }

    void SetIdle()
    {
        Debug.Log("Snake returning to idle");
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Idle");
    }

    public void PlayerDetected(Transform detectedPlayer)
    {
        player = detectedPlayer;
        Debug.Log("Player detected!");
        Attack(); // Langsung menyerang ketika player terdeteksi
    }

    public void PlayerLost()
    {
        player = null;
        SetIdle();
        Debug.Log("Player lost!");
    }

    void SnakeAttackOn()
    {
        CollSnakeAttack.enabled = true;
    }

    void SnakeAttackOff()
    {
        CollSnakeAttack.enabled = false;
    }

    public void EnemyIsAttcked()
    {
        animator.SetTrigger("Dead");
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
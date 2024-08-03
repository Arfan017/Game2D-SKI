using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 5f;
    private Vector3 startPosition;
    private bool movingRight = true;
    private Animator animator;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (movingRight)
        {
            if (transform.position.x <= startPosition.x + moveDistance)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(-3.73f, 3.73f, 3.73f);
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if (transform.position.x >= startPosition.x)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                transform.localScale = new Vector3(3.73f, 3.73f, 3.73f);
            }
            else
            {
                movingRight = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Implementasi logika ketika musuh menyentuh pemain
            Debug.Log("By Enemy Controller Player hit by enemy!");
            // Misalnya, kurangi health pemain atau restart level
        }
    }

    public void EnemyIsAttcked()
    {
        // moveDistance = 0f;
        moveSpeed = 0f;
        animator.SetTrigger("Hit");
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
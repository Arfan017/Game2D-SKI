using UnityEngine;

public class ChestsController : MonoBehaviour
{
    public GameObject keyPrefab; // Prefab kunci yang akan dikeluarkan
    public Transform keySpawnPoint; // Titik spawn kunci
    public float keyEjectionForce = 5f; // Gaya untuk "melontarkan" kunci
    public string playerTag = "Player"; // Tag dari objek player

    private Animator animator; // Komponen Animator untuk animasi chest
    private bool isOpen = false; // Flag untuk mengecek apakah chest sudah terbuka

    void Start()
    {
        // Dapatkan komponen Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the chest!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !isOpen)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        isOpen = true;
        // Trigger animasi chest terbuka
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }

        // Panggil fungsi untuk spawn kunci setelah delay singkat
        Invoke("SpawnKey", 0.5f); // Delay 0.5 detik, sesuaikan dengan animasi Anda
    }

    void SpawnKey()
    {
        if (keyPrefab != null && keySpawnPoint != null)
        {
            // Instantiate kunci
            GameObject key = Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);

            // Tambahkan gaya untuk "melontarkan" kunci
            Rigidbody2D keyRb = key.GetComponent<Rigidbody2D>();
            if (keyRb != null)
            {
                Vector2 ejectionDirection = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
                keyRb.AddForce(ejectionDirection * keyEjectionForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("Key prefab or spawn point is not set!");
        }
    }
}
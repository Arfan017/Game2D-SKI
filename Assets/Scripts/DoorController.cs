using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public string nextSceneName; // Nama scene yang akan dimuat
    public float transitionDelay = 1f; // Waktu delay sebelum pindah scene
    public string playerTag = "Player"; // Tag dari objek player
    private Animator animator; // Komponen Animator untuk animasi pintu
    private bool isPlayerNear = false; // Flag untuk mengecek apakah player dekat
    private bool isOpen = false; // Flag untuk mengecek apakah pintu sudah terbuka
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        // Dapatkan komponen Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the door!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !isOpen)
        {
            OpenDoor();
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerNear = false;
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        // Trigger animasi pintu terbuka
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
    }

    void Update()
    {
        // Cek jika player dekat dan pintu sudah terbuka
        if (isPlayerNear && isOpen)
        {
            // Mulai proses perpindahan scene
            StartCoroutine(TransitionToNextScene());
        }
    }

    System.Collections.IEnumerator TransitionToNextScene()
    {
        // Tunggu selama transitionDelay
        yield return new WaitForSeconds(transitionDelay);
        gameManager.IsMenang();
        // Time.timeScale = 0;
        // Pindah ke scene berikutnya
        // SceneManager.LoadScene(nextSceneName);
    }
}
using UnityEngine;
using System.Collections;

public class SurpriseBox : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 5f;
    public Sprite spriteNotActived;
    public GameObject[] itemPrefab;
    public Transform spawnPoint;
    private Vector3 originalPosition;
    private bool isHit = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            // Cek apakah pemain membentur dari bawah
            if (collision.contacts[0].normal.y > 0.5f)
            {
                isHit = true;
                StartCoroutine(MoveBox());
                SpawnItem();
            }
        }
    }

    IEnumerator MoveBox()
    {
        // Gerak ke atas
        Vector3 targetPosition = originalPosition + Vector3.up * moveDistance;
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Gerak kembali
        while (transform.position != originalPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteNotActived;
            yield return null;
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            // Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            int ValueRandom = UnityEngine.Random.Range(0, itemPrefab.Length);
            // int ValueRandom = 0;
            if (ValueRandom == 0)
            {
                itemPrefab[ValueRandom].SetActive(true);
            }
            else
            {
                itemPrefab[ValueRandom].SetActive(true);
            }
        }
    }
}
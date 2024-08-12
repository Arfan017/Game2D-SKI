using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item collected!");
            // Panggil metode untuk menambah skor
            GameManager.Instance.AddScore(scoreValue);

            // Hancurkan item setelah diambil
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private SnakeEnemy parentSnake;

    void Start()
    {
        // Mendapatkan referensi ke script SnakeEnemy pada parent
        parentSnake = GetComponentInParent<SnakeEnemy>();

        if (parentSnake == null)
        {
            Debug.LogError("SnakeEnemy script tidak ditemukan pada parent object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentSnake != null)
        {
            // Memberi tahu snake bahwa player telah memasuki area
            parentSnake.PlayerDetected(other.transform);
            Debug.Log("Player telah memasuki area!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentSnake != null)
        {
            // Memberi tahu snake bahwa player telah keluar dari area
            parentSnake.PlayerLost();
            Debug.Log("Player telah keluar dari area!");
        }
    }
}

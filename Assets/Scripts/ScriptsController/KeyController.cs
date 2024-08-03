using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int keyValue = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Tambahkan kunci ke inventory player atau increment jumlah kunci
            // Misalnya: other.GetComponent<PlayerInventory>().AddKey();
            // Panggil metode untuk menambah skor
            GameManager.Instance.AddKey(keyValue);
            Destroy(gameObject);
        }
    }
}

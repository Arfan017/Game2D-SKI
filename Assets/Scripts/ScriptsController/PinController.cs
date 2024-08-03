using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Button rightButton;
    public Button leftButton;
    public string stopTagPosNow;
    private bool isMoving = false;
    Animator animator;
    private Vector3 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Menambahkan listener ke button kanan
        if (rightButton != null && leftButton != null)
        {
            rightButton.onClick.AddListener(MoveRight);
            // leftButton.onClick.AddListener(MoveLeft);
        }
    }

    void Update()
    {

    }

    void MoveRight()
    {
        Debug.Log("Move Right");
        if (stopTagPosNow == "pos1")
        {
            animator.SetBool("ToPost2", true);
        }
        else if (stopTagPosNow == "pos2")
        {
            animator.SetBool("ToPost3", true);
        }
        else if (stopTagPosNow == "pos3")
        {
            animator.SetBool("ToPost4", true);
        }
    }

    void MoveLeft()
    {
        Debug.Log("Move Left");
        isMoving = true;
        moveDirection = Vector3.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Berhenti ketika bertemu dengan objek ber-tag pos1
        if (other.CompareTag("pos1"))
        {
            Debug.Log("Pos1");
            stopTagPosNow = "pos1";
            return;
        }
        else if (other.CompareTag("pos2"))
        {
            Debug.Log("Pos2");
            stopTagPosNow = "pos2";
            animator.SetBool("ToPost2", false);
            // animator.SetBool("ToIdle", true);
        }
        else if (other.CompareTag("pos3"))
        {
            Debug.Log("Pos3");
            stopTagPosNow = "pos3";
            animator.SetBool("ToPost3", false);
        }
    }
}

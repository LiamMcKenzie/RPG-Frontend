using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component of your character.
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Detect left-click input (You can customize this based on your input setup).
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button.
        {
            // Trigger the "Attack" animation.
            animator.SetTrigger("Attack");
        }
    }
}
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
        // Axe Downward Swing
        if (Input.GetKey(KeyCode.Z)) 
        {
            animator.SetTrigger("Axe_Attack_Swing"); // Trigger animation toggle
        }

        //Axe Side Slash
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger("Axe_Attack_Slash");
        }

        //Staff Cast Attack
        if (Input.GetKey(KeyCode.C))
        {
            animator.SetTrigger("Staff_Attack_Cast");
        }

        //Bow Idle Attack
        if (Input.GetKey(KeyCode.V))
        {
            animator.SetTrigger("Bow_Attack_Idle");
        }

        //Dancing Animation
        if (Input.GetKey(KeyCode.P))
        {
            animator.SetTrigger("Dance_01");
        }
    }
}
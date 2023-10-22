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
        // Axe Downward Strike
        if (Input.GetKey(KeyCode.Z)) 
        {
            animator.SetTrigger("Axe_Attack_01"); // Trigger animation toggle
        }

        //Axe Side Slash
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetTrigger("Axe_Attack_02");
        }

        //Staff Thrust Attack
        if (Input.GetKey(KeyCode.C))
        {
            //animator.SetTrigger("Death_FallDown");
        }
    }
}
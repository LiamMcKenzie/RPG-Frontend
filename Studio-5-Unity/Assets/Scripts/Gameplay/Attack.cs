using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private Animator animator;

    /// <summary>
    /// Initialize the Animator component of the character.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Check for key presses and trigger corresponding animations based on key-to-animationBool mapping.
    /// </summary>
    private void Update()
    {
        foreach (var kvp in attackKeyMap)
        {
            if (Input.GetKeyDown(kvp.Key))
            {
                StartCoroutine(TriggerAnimation(kvp.Value));
            }
        }
    }

    /// <summary>
    /// Animation bool condition set to true and returns to false after a delay 
    /// </summary>
    /// <param name="animationCondition">Boolean name (kvp.Value) used in the character animator (e.g Axe_Slash)</param>
    /// <returns></returns>
    IEnumerator TriggerAnimation(string animationCondition)
    {
        animator.SetBool(animationCondition, true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool(animationCondition, false);
    }

    /// <summary>
    /// A dictionary that maps KeyCodes to animation conditions for triggering specific attacks.
    /// </summary>
    private Dictionary<KeyCode, string> attackKeyMap = new Dictionary<KeyCode, string>
    {
        { KeyCode.Z, "Axe_Swing" },
        { KeyCode.X, "Axe_Slash" },
        { KeyCode.C, "Staff_Cast" },
        { KeyCode.V, "Bow_Fire" },
        { KeyCode.P, "Dance" }
    };
}

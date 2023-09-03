using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Variables")]
    public float moveSpeed;  //Controls the players movement speed
    private PlayerStates currentState; //Check for current animation needed

    [Header("Jump Variables")]
    public float jumpHeight; //Controls the players jump height
    public bool grounded;

    [Header("Components required for movement/animation")]
    public Rigidbody rb;
    public SpriteRenderer sr;
    public BoxCollider bc;
    public Animator animator;

    /// <summary>
    /// Possible animation states for the player character in the animator.
    /// </summary>
    public enum PlayerStates
    { IDLE, WALK, ATTACK, DIE }

    /// <summary>
    /// Handles the switching and setting of the current animation state.
    /// </summary>
    PlayerStates CurrentState
    {
        set
        {
            currentState = value;

            switch (currentState)
            {
                case PlayerStates.IDLE:
                    animator.Play("Idle");
                    break;
                case PlayerStates.WALK:
                    animator.Play("Walk");
                    break;
                case PlayerStates.ATTACK:
                    animator.Play("Attack");
                    break;
                case PlayerStates.DIE:
                    animator.Play("Die");
                    break;
                default:
                    break;
            }
        }
    }
    
    /// <summary>
    /// Instantiates player/terrain objects
    /// </summary>
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        grounded = false;
    }

    /// <summary>
    /// Updates the player's movement, input handling, and animation based on user input.
    /// </summary>
    void Update()
    {   
        //Controls movement input for the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, z) * moveSpeed;
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;

        jumpCheck();   //Check if jumping
        FlipSprite(x); //Check if sprite needs flipped
        AnimationCheck(x, z); //Check if animation state needs to change
    }

    /// <summary>
    /// Controls switching of sprite direction.
    /// If x less than 0, sprite flipped horizontally left.
    /// If x greater than 0,  sprite flipped horizontally right.
    /// No flip occurs if x is 0.
    /// </summary>
    /// <param name="x">The horizontal movement input value (usually obtained from Input.GetAxis("Horizontal")). </param>
    private void FlipSprite(float x)
    {
        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }
    }

    /// <summary>
    /// Controls the switching of animation states based on the current input values.
    /// </summary>
    /// <param name="x">The horizontal movement input value (usually obtained from Input.GetAxis("Horizontal")).</param>
    /// <param name="z">The vertical movement input value (usually obtained from Input.GetAxis("Vertical")).</param>
    private void AnimationCheck(float x, float z)
    {
        if (x != 0 || z != 0) //If an input is being pressed
        {
            CurrentState = PlayerStates.WALK;
            animator.SetFloat("xMove", x);
            animator.SetFloat("zMove", -z);
        }
        else
        {
            CurrentState = PlayerStates.IDLE;
        }
    }

    /// <summary>
    /// Checks if the player has pressed space to jump. Will only jump while grounded
    /// </summary>
    private void jumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * (jumpHeight * -(Physics.gravity.y)), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// When the player collider hits the ground, enable jumping again
    /// </summary>
    /// <param name="collision">Floor/Object below the player</param>
    private void OnCollisionEnter(Collision collision)
    {
        grounded = !grounded;
        Debug.Log("Grounded State: " + grounded);
    }

    /// <summary>
    /// When the player jumps, disable ability to jump again
    /// </summary>
    /// <param name="collision">Floor/Object below the player</param>
    private void OnCollisionExit(Collision collision)
    {
        grounded = !grounded;
        Debug.Log("Grounded State: " + grounded);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    [Header("Terrain Collision Variables")]
    public float groundDist; //Snapping distance to the ground
    public LayerMask terrainLayer;

    [Header("Player Movement Variables")]
    public float moveSpeed;  //Controls the players movement speed
    private PlayerStates currentState; //Check for current animation needed

    [Header("Jump Variables")]
    public float jumpHeight;
    public bool grounded;

    [Header("Components required for movement/animation")]
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;

    /// <summary>
    /// Enumerates the possible animation states for the player character in the animator.
    /// </summary>
    public enum PlayerStates
    {
        IDLE,
        WALK,
        ATTACK,
        DIE
    }

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
        animator = GetComponent<Animator>();
        grounded = false;
    }

    /// <summary>
    /// Updates the player's movement, input handling, and animation based on user input and terrain collisions.
    /// </summary>
    void Update()
    {   
        //CastTerrain(); //Raycast for the terrain layer to check player collision

        //Controls movement input for the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, z) * moveSpeed;
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;


        jumpCheck();
        FlipSprite(x); //Check if sprite needs flipped
        AnimationCheck(x, z); //Check if animation state needs to change
    }

    /// <summary>
    /// Casts a ray to check if the player is below the terrain height and adjusts the player's position to snap to the ground.
    /// </summary>   
    private void CastTerrain()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
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

    private void jumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}

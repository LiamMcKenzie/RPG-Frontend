using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controls the players movement speed,
    public float moveSpeed;
    public float groundDist; //Snapping distance to the ground
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;
    private PlayerStates currentState; //Check for current animation needed

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CastTerrain(); //Raycast for the terrain layer to check player collision

        //Controls movement input for the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, z);

        rb.velocity = moveDir * moveSpeed;

        FlipSprite(x); //Check if sprite needs flipped
        AnimationCheck(x, z); //Check if animation state needs to change

    }

    //If player is below the terrain height, it will snap the y pos of the player to the ground
    //(Note : Does not work completely and may cause issues with further development, need to find another solution for this collision)
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

    //Flips sprite direction depending on direction of movement
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

    //Controls the switching of animation states based on current input values
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

    //States for animation set in the player animator
    public enum PlayerStates
    {
        IDLE,
        WALK,
        ATTACK,
        DIE
    }

    //Handles the switching and setting of the current animation state
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controlls the players movement speed,
    //look at using 'agility' skill to potentially increase this during gameplay 
    public float speed;
    public float groundDist; //Snapping distance to the ground

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast for the terrain layer to check player collision
        //If player is below the terrain height, it will snap the y pos of the player to the ground
        //(Dont need to check if above terrain as Rigidbody has gravity)
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


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, 0, z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 200f);
        }

        if (moveDir.magnitude > 1)
        {
            moveDir.Normalize();
        }

        rb.velocity = moveDir * speed;

        //Flips sprite direction depending on direction of movement
        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0){
            sr.flipX = false;
        }
    }
}

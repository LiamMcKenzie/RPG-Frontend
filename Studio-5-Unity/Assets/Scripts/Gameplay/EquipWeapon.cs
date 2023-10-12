/*
This code is a work in-progress
It mostly just contains the basic framework and my though process of how this should work at this stage of development
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EquipWeapon : MonoBehaviour
{
    //Player Variables
    [Header("Player")]
    [Header("Weapons")]
    public GameObject playerWeapon;
    [Header("Levels")]
    public int playerLevel;
    public int currentWeaponLevel;
    [Header("Boolean States")]
    public bool hasWeapon;

    [Header("")] //Additional spacing of inspector groups

    //Pickup weapon Variables
    [Header("Pickup")]
    [Header("Weapons")]
    public GameObject pickupWeapon;
    [Header("Levels and XP")]
    public int pickupLevel;
    [Header("Boolean States")]
    public bool canEquip;

    // Start is called before the first frame update
    void Start()
    {
        //Update in the future with GetRequests from the API (currently placeholders)
        //eg        playerLevel = Get(Character.level)
        //   currentWeaponLevel = Get(Character.item.level)
        playerLevel = 2;
        currentWeaponLevel = 1;
        hasWeapon = WeaponEnabledCheck(); 
    }

    /// <summary>
    /// If the player has already equipped an item or not
    /// </summary>
    /// <returns>(Type:Bool)  Checks if the players current weapon gameobject is active in the inspector</returns>
    private bool WeaponEnabledCheck()
    {
        return playerWeapon.gameObject.activeSelf;
    }

    /// <summary>
    /// Gets the level of the weapon in the current location
    /// </summary>
    /// <returns>(Type:int) Level of weapon returned from API Get request</returns>
    private int GetWeaponLevel(GameObject weapon)
    {
        //itemLevel = weapon.level;
        int itemLevel;
        //itemLevel = Get(Location.item.level); REPLACE API REQUEST CODE HERE
        itemLevel = 4;
        return itemLevel;
    }

    /// <summary>
    /// Gets the current level of the item you want to pickup
    /// Checks if the player level is equal to or grater than the item level
    /// Enables the characters weapon in the inspector
    /// Swaps the player weapon gameobject to the pickup item
    /// </summary>
    public void EquipWeaponCheck(MeshRenderer newTexture, MeshFilter newMesh)
    {
        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        if (!WeaponEnabledCheck())
        {
            playerWeapon.SetActive(true);
        }

        meshRenderer = playerWeapon.GetComponent<MeshRenderer>();
        meshFilter = playerWeapon.GetComponent<MeshFilter>();

        meshRenderer.material = newTexture.material;
        meshFilter.mesh = newMesh.mesh;

        hasWeapon = WeaponEnabledCheck();
        //pickupLevel = GetWeaponLevel(mr, mf);

        /*
        if (playerLevel >= pickupLevel)
        {
            canEquip = true;
                
            if (!WeaponEnabledCheck())
            {
                playerWeapon.SetActive(true); 
            }

            mesh = playerWeapon.GetComponent<MeshFilter>();
            mesh = mf;
            texture = playerWeapon.GetComponent<MeshRenderer>();
            texture = mr;
            playerWeapon = pickupWeapon; //Swap the player weapon gameobject
            Debug.Log("You successfully pickup the weapon");
        }
        //LOOK AT ADDING A PLAYER CHECK IF THEY WANT TO SWAP WEAPON OR NOT
        else
        {
            Debug.Log("Your character is not a high enough level to pick this up");
        }*/

    }
}
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
    [Header("Weapons")]
    public GameObject playerWeapon;
    [Header("Boolean States")]
    public bool hasWeapon;

    // Start is called before the first frame update
    void Start()
    {
        //Update in the future with GetRequests from the API (currently placeholders)
        //eg        playerLevel = Get(Character.level)
        //   currentWeaponLevel = Get(Character.item.level)

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
    }
}
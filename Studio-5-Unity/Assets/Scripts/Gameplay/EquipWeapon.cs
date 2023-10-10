using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    //Player Variables
    [Header("Player")]
    [Header("Weapons")]
    public GameObject playerWeapon;
    [Header("Levels and XP")]
    public int playerXP;
    public int playerLevel;
    public int currentWeaponLevel;
    [Header("Boolean States")]
    public bool weaponEquipped;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

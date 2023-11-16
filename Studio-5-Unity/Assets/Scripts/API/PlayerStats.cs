using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public string name;
    public int level;
    public int health;
    public int attack;
    public int defense;
    public int mana;
    public int agility;
    public void Start()
    {
        Instance = this;
    }
}

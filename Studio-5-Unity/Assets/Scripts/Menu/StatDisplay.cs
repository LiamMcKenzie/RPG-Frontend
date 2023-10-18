using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    public TMP_Text TEXT;

    // Update is called once per frame
    void Update()
    {
        TEXT.text = ("Name: " + PlayerStats.Instance.name + "<br>" +
                    "Level: " + PlayerStats.Instance.level +"<br>" +
                    "Health: " + PlayerStats.Instance.health +"<br>" +
                    "Attack: " + PlayerStats.Instance.attack +"<br>" +
                    "Defence: " + PlayerStats.Instance.defense +"<br>" +
                    "Mana: " + PlayerStats.Instance.mana +"<br>" +
                    "Agility: " + PlayerStats.Instance.agility

                    );
    }
}

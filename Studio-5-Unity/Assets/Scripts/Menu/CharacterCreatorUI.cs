using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreatorUI : MonoBehaviour
{

    public enum MenuState
    {
        characterSelect,
        classSelect,
        stats,
        gender,
        name,
        finish
    }

    public List <GameObject> hiddenPanels = new List<GameObject>();

    public MenuState currentMenuState;


    public void ChangePanels(int newMenuState)
    {
        //MenuState newMenuState
        currentMenuState = (MenuState)newMenuState; //enums are weird. This sets the current enum value to the specified index
        //hiddenPanels[(int)currentMenuState]

        for(int i = 0; i < hiddenPanels.Count; i++)
        {
            hiddenPanels[i].SetActive(i + 1 > (int)currentMenuState);
        }
    }

    /* public void Update()
    {
        ChangePanels();
    } */
}

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

}

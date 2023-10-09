using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectActive : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject deselectedObject;
    public Button enterButton;

    public void DisableObject ()
    {
        selectedObject.SetActive(false);
    }

    public void EnableObject ()
    {
        selectedObject.SetActive(true);
    }

    public void CloseAndEnable()
    {
        deselectedObject.SetActive(false);
        selectedObject.SetActive(true);
        KeyboardManager.Instance.enterButton = enterButton;
    }
}

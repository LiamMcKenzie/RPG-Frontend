using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField tmpText;
    public APITest apiTest;
    public string playerName = "Liam02";
  

    public void Update()
    {
        playerName = tmpText.text;
    }

    public void MakeAPIRequest()
    {
        StartCoroutine(apiTest.RegisterPlayer(playerName));
        //StartCoroutine(SendRequest());
        
    }
}

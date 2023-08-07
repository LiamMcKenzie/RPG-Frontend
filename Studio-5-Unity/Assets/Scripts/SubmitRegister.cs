using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField usernameTMP;
    public TMP_InputField passwordTMP;
    public APITest apiTest;
    public string username = "username";
    public string password = "password";
  

    public void Update()
    {
        username = usernameTMP.text;
        password = passwordTMP.text;
    }

    public void MakeAPIRequest()
    {
        StartCoroutine(apiTest.RegisterPlayer(username, password));
        //StartCoroutine(SendRequest());
        
    }
}

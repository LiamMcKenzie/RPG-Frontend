using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField usernameTMP;
    public TMP_InputField passwordTMP;
    public APIManager apiManager;
    public string username = "username";
    public string password = "password";
    public string path;
  

    public void Update()
    {
        username = usernameTMP.text;
        password = passwordTMP.text;
    }

    public void RegisterRequest()
    {
        StartCoroutine(apiManager.PostRequest(username, password, "auth/register"));
    }

    public void LoginRequest()
    {
        StartCoroutine(apiManager.PostRequest(username, password, "auth/login"));
    }
}

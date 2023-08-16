using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField usernameTMP;
    public TMP_InputField passwordTMP;
    public TMP_InputField confirmPasswordTMP;
    public APIManager apiManager;

    private string username = "";
    private string password = "";
    private string confirmPassword = "";

    public void Update()
    {
        username = usernameTMP.text;
        password = passwordTMP.text;
        confirmPassword = confirmPasswordTMP.text;
    }

    public void RegisterRequest()
    {
        if (password == confirmPassword)
        {
            StartCoroutine(apiManager.RegisterAndLogin(username, password));
        }
        else
        {
            Debug.LogError("Passwords do not match!");
        }
    }

    public void LoginRequest()
    {
        StartCoroutine(apiManager.PostRequest(username, password, "auth/login"));
    }
}

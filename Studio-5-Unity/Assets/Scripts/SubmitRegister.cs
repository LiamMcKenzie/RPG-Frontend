using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField emailInputField; // Reference to the email input field
    public TMP_InputField passwordTMP;
    public TMP_InputField confirmPasswordTMP;
    public APIManager apiManager;

    private string email = ""; // String to hold user's entered email
    private string password = "";
    private string confirmPassword = "";

    public void Update()
    {
        email = emailInputField.text; // Capture email input
        password = passwordTMP.text;
        confirmPassword = confirmPasswordTMP.text;
    }

    public void RegisterRequest()
    {
        if (password == confirmPassword)
        {
            StartCoroutine(apiManager.RegisterAndLogin(email, password)); // Pass email instead of username
        }
        else
        {
            Debug.LogError("Passwords do not match!");
        }
    }

    public void LoginRequest()
    {
        StartCoroutine(apiManager.PostRequest(email, password, "auth/login")); // Pass email instead of username
    }
}

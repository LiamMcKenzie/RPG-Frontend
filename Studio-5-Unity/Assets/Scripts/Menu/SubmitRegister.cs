using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitRegister : MonoBehaviour
{
    public TMP_InputField usernameTMP;
    public TMP_InputField passwordTMP;
    public TMP_InputField confirmPasswordTMP;
    public TMP_Text debugDialog;
    public string username = "username";
    public string password = "password";
    public string path;
    private Button button;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
  

    public void Update()
    {
        username = usernameTMP.text;
        password = passwordTMP.text;
        if(username.Length < 3 || password.Length < 3)
        {
            button.interactable = false;
        }else
        {
            button.interactable = true;
        }
    }

    public void RegisterRequest()
    {
        if(passwordTMP.text == confirmPasswordTMP.text)
        {
            StartCoroutine(APIManagerNew.instance.LoginRequest(username, password, "auth/register"));
            //APIManagerNew.instance.StartCoroutine(LoginRequest(username, password, "auth/register"));
        }else{
            debugDialog.text = "Passwords do not match. Please make sure both passwords are the same.";
        }
    }

    public void LoginRequest()
    {
        StartCoroutine(APIManagerNew.instance.LoginRequest(username, password, "auth/login"));
        //StartCoroutine(LoginRequest(username, password, "auth/login"));
        //APIManagerNew.instance.LoginRequest(username, password, "auth/login");
    }


  
}

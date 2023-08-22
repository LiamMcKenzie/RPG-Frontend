using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LoginErrorMessage : MonoBehaviour
{
    private TMP_Text debugDialog;
    // Start is called before the first frame update
    void Start()
    {
        debugDialog = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(APIManager.Instance.requestResult == UnityWebRequest.Result.ProtocolError)
        {
            debugDialog.text = "Account not found. Please check your login details or create an account if you're a new user.";
        }
    }
}

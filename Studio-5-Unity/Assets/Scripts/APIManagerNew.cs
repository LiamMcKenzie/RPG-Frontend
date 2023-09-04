using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManagerNew : MonoBehaviour
{
    public static APIManagerNew instance;
    public LoginData myData;
    public bool isLoading = false;
    public string responseText;
    public string url = "http://localhost:3000/api/v1";
    //http://studio6-api-host.op-bit.nz/api/v1

    [System.Serializable]
    public class LoginData
    {
        public string msg;
        public string token;
    }

    public void Start()
    {
        instance = this;
    }

    //REUSABLE CODE
    public IEnumerator ReturnResult(UnityWebRequest request)
    {
        isLoading = true;
        yield return request.SendWebRequest();
        isLoading = false;
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            responseText = request.downloadHandler.text;
            Debug.Log(responseText);
        }
    }

    //REQUESTS 
    public void LoginRequest(string newusername, string newpassword, string path)
    {
        
        //Creating json data
        var jsonData = new
        {
            username = newusername,
            password = newpassword
        };
        //string jsonData = $"{{\"username\": \"{username}\", \"password\": \"{password}\"}}";
        string jsonRequestBody = JsonUtility.ToJson(jsonData);
        
        //Creating Request
        UnityWebRequest request = UnityWebRequest.Post($"{url}/{path}", jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");

        //Sending request
        StartCoroutine(ReturnResult(request)); // sends a web request
        myData = JsonUtility.FromJson<LoginData>(responseText); //uses the response text from request to create a json object
        Debug.Log(myData);
        APIToken.token = myData.token; //sets the api token
        Debug.Log(APIToken.token);

        //Login/Register switching
        if(request.result == UnityWebRequest.Result.Success && path == "auth/register"){
            LoginRequest(newusername, newpassword, "auth/login");
        }

        if(request.result == UnityWebRequest.Result.Success && path == "auth/login"){
            //SceneManager.LoadScene("CharacterCreate");
        }

        request.Dispose();
    }

}

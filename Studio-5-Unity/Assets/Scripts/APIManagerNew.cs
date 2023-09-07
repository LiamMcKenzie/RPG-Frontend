using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class APIManagerNew : MonoBehaviour
{
    public string responseText;
    public static APIManagerNew instance;
    public LoginData myData;
    public bool isLoading = false;
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


    //REQUESTS 
    public IEnumerator LoginRequest(string newusername, string newpassword, string path)
    {
        
        string jsonRequestBody = $"{{\"username\": \"{newusername}\", \"password\": \"{newpassword}\"}}";
        
        //Creating Request
        UnityWebRequest request = UnityWebRequest.Post($"{url}/{path}", jsonRequestBody);
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody); //idk what these two lines do
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.SetRequestHeader("Content-Type", "application/json"); //adds the request header (this function is very important)

        //Sending request
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
        }

        myData = JsonUtility.FromJson<LoginData>(responseText); //uses the response text from request to create a json object
        APIToken.token = myData.token; //sets the api token

        //Login/Register switching
        if(request.result == UnityWebRequest.Result.Success && path == "auth/register"){
            LoginRequest(newusername, newpassword, "auth/login");
        }

        if(request.result == UnityWebRequest.Result.Success && path == "auth/login"){
            SceneManager.LoadScene("CharacterCreate");
        }

        request.Dispose();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class APIManager : MonoBehaviour
{
    public string responseText;
    public static APIManager instance;
    public RequestData myData;
    public bool isLoading = false;
    public string url = "http://localhost:3000/api/v1";
    //http://studio6-api-host.op-bit.nz/api/v1

    [System.Serializable]
    public class RequestData
    {
        public string msg;
        public string token;
    }

    public void Start()
    {
        instance = this;
    }

    public IEnumerator SendRequest(UnityWebRequest request)
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
            myData = JsonUtility.FromJson<RequestData>(responseText); //uses the response text from request to create a json object
            APIToken.token = myData.token; //sets the api token
        }
        yield return null;
    }

    UnityWebRequest request;
    public IEnumerator CreateRequest(string method, string path, string jsonString = "")
    {
        //Creating Request
        request = null;
        
        switch(method)
        {
            case "GET" : 
                request = UnityWebRequest.Get($"{url}/{path}");
                break;
            
            case "POST" : 
                Debug.Log($"{url}/{path}" + " " + jsonString);
                request = UnityWebRequest.Post($"{url}/{path}", jsonString);
                break;
            
            case "UPDATE" : 
                //request = UnityWebRequest.Update($"{url}/{path}", jsonRequestBody);
                break;

            default :
                request = null;
                break;
        }
        
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString); //idk what these two lines do
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        if(request != null){
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + APIToken.token);
            yield return StartCoroutine(SendRequest(request)); // sends a web request
        }else
        {
            yield return null;
        }
        


    }


    //REQUESTS 
    public IEnumerator LoginRequest(string newusername, string newpassword, string path)
    {
        string jsonRequestBody = $"{{\"username\": \"{newusername}\", \"password\": \"{newpassword}\"}}";
        
        yield return StartCoroutine(CreateRequest("POST",path,jsonRequestBody));

        //Login/Register switching
        if(request.result == UnityWebRequest.Result.Success && path == "auth/register"){
            yield return StartCoroutine(LoginRequest(newusername, newpassword, "auth/login"));
        }

        if(request.result == UnityWebRequest.Result.Success && path == "auth/login"){
            SceneManager.LoadScene("CharacterCreate");
        }

        request.Dispose();
    }


}

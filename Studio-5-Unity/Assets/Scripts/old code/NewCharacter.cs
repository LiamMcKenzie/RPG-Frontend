using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NewCharacter : MonoBehaviour
{
    public UnityWebRequest.Result requestResult;
    public string responseText;

    public IEnumerator PostRequest(string name, string buildId, string gender)
    {
        //playerName = "Liam02";
        // Define the data to send in JSON format
        
        
        //string jsonData = "{\"symbol\": \"" + username + "\", \"faction\": \"COSMIC\"}";
        string jsonData = $"{{\"name\": \"{name}\", \"gender\": \"{gender}\", \"buildId\": \"{buildId}\"}}";


        // Set up the request
        UnityWebRequest request = UnityWebRequest.Post($"http://localhost:3000/api/v1/character/create", jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + APIToken.token);
        //APIManager.Instance.isLoading = true;
        //isLoading = true;
        // Send the request
        yield return request.SendWebRequest();
        //APIManager.Instance.isLoading = false;

        // Handle the response
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            // You can process the responseText as needed
        }

        Debug.Log(request.result + ", ");

        requestResult = request.result;

        request.Dispose();

    }

    public IEnumerator NewSeedBuild()
    {
        UnityWebRequest request = UnityWebRequest.Get($"http://localhost:3000/api/v1/builds/create");

        request.SetRequestHeader("Authorization", "Bearer " + APIToken.token);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            Debug.Log("Request successful!");
        }
    }

    /*
    public IEnumerator NewPostRequest()
    {
        string requestBody = $"{{\"name\": \"{"bob222"}\", \"gender\": \"{"MALE"}\", \"buildId\": \"{1}\"}}";


        UnityWebRequest request = UnityWebRequest.Post($"http://localhost:3000/api/v1/character/create", requestBody);

        request.SetRequestHeader("Authorization", "Bearer " + APIToken.token);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Request successful!");
        }
        
    }*/

    public IEnumerator NewPostRequest()
    {
        string name = "bob222";
        string gender = "MALE";
        int buildId = 1;
        // Create a JSON object to represent the data
        /*var requestData = new
        {
            
        };*/
        //string jsonRequestBody = $"{{\"name\": \"{name}\", \"gender\": \"{gender}\", \"buildId\": \"{buildId}\"}}";
       string jsonRequestBody = $"{{\"name\": \"bob\", \"gender\": \"MALE\", \"buildId\": 1}}";



        // Convert the object to a JSON string
        //string jsonRequestBody = JsonUtility.ToJson(requestData);

        // Create a POST request with the API endpoint
        UnityWebRequest request = UnityWebRequest.Post("http://localhost:3000/api/v1/character/create", jsonRequestBody);


        // Attach the request body as a byte array
        byte[] requestBodyBytes = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
        request.uploadHandler = new UploadHandlerRaw(requestBodyBytes);
        
        // Set headers
        request.SetRequestHeader("Authorization", "Bearer "+ APIToken.token);
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check for errors
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


    public IEnumerator NewGetAllCharacters()
    {
        UnityWebRequest request = UnityWebRequest.Get($"http://localhost:3000/api/v1/character");

        request.SetRequestHeader("Authorization", "Bearer " + APIToken.token);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            Debug.Log("Request successful!");
        }
    }
    
    /*
    private IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Post($"http://localhost:3000/api/v1/character/create", jsonData);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiToken);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            
            dataWrapper = JsonUtility.FromJson<DataWrapper>(responseText);
            // Now you can access the properties of MyData through the DataWrapper class
            PlayerData myData = dataWrapper.data;
            Debug.Log("Account ID: " + myData.accountId);
            Debug.Log("Symbol: " + myData.symbol);
            Debug.Log("Headquarters: " + myData.headquarters);
            Debug.Log("Credits: " + myData.credits);
            Debug.Log("Starting Faction: " + myData.startingFaction);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }*/
}

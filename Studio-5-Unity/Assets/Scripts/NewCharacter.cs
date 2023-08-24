using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NewCharacter : MonoBehaviour
{
    public UnityWebRequest.Result requestResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}

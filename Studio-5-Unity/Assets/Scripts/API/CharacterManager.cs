using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public UnityWebRequest.Result requestResult;
    public string responseText;

    void Start()
    {
        StartCoroutine(NewGetAllCharacters());
        //StartCoroutine(characterManager.NewSeedBuild());
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

    public IEnumerator NewPostRequest(string name, string gender)
    {
        int buildId = 1;

        //Debug.Log(APIToken.token);
        string jsonRequestBody = $"{{\"name\": \"{name}\", \"gender\": \"{gender}\", \"buildId\": {buildId}}}";

        //Debug.Log(jsonRequestBody);

        string path = "character/create";

        yield return StartCoroutine(APIManager.instance.CreateRequest("POST",path,jsonRequestBody));

        Debug.Log(APIManager.instance.responseText);
    }


    public IEnumerator NewGetAllCharacters()
    {
        string path = "character";
        yield return StartCoroutine(APIManager.instance.CreateRequest("GET",path));

        Debug.Log(APIManager.instance.responseText);
    }
}

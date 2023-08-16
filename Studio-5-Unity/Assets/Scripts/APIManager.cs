using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    //private string jwtSecret = "Pazzw0rd123";
    private string authToken;
    private UnityWebRequest.Result requestResult;

    [System.Serializable]
    public class PlayerData
    {
        public string accountId;
        public string gender;
        public string playerClass;
    }

    [System.Serializable]
    public class DataWrapper
    {
        public PlayerData data;
    }

    public static APIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        MakeAPIRequest();
    }

    public void MakeAPIRequest()
    {
        StartCoroutine(GetPlayerData());
    }

    private IEnumerator GetPlayerData()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/api/v1/my/agent");
        request.SetRequestHeader("Content-Type", "application/json");
        AddAuthHeader(request);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);

            DataWrapper dataWrapper = JsonUtility.FromJson<DataWrapper>(responseText);
            PlayerData playerData = dataWrapper.data;

            Debug.Log("Account ID: " + playerData.accountId);
            Debug.Log("Gender: " + playerData.gender);
            Debug.Log("Player Class: " + playerData.playerClass);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    public IEnumerator RegisterAndLogin(string username, string password)
    {
        yield return StartCoroutine(PostRequest(username, password, "auth/register"));

        if (requestResult == UnityWebRequest.Result.Success)
        {
            yield return StartCoroutine(PostRequest(username, password, "auth/login"));

            if (requestResult == UnityWebRequest.Result.Success)
            {
                authToken = ParseAuthTokenFromResponse();
            }
        }
    }

    public IEnumerator PostRequest(string username, string password, string path)
    {
        string jsonData = $"{{\"username\": \"{username}\", \"password\": \"{password}\"}}";

        UnityWebRequest request = UnityWebRequest.Post($"http://localhost:3000/api/v1/{path}", jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);

            if (path == "auth/register")
            {
                Debug.Log("Registration successful!");
            }
            else if (path == "auth/login")
            {
                Debug.Log("Login successful!");
            }
        }

        requestResult = request.result;
    }


    private string ParseAuthTokenFromResponse()
    {

        string authToken = "parsed_token_here";
        return authToken;
    }

    // Use this function to add the authentication header to requests
    private void AddAuthHeader(UnityWebRequest request)
    {
        if (!string.IsNullOrEmpty(authToken))
        {
            // Create the JWT token using the JWT secret
            string jwtToken = CreateJWTToken();

            request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        }
    }

    private string CreateJWTToken()
    {
        // You need to implement JWT token creation here using your JWT secret
        // You can use a library like 'JWT' or implement it manually
        // This is a simplified example and not secure for production
        string jwtToken = "your_generated_jwt_token_here";
        return jwtToken;
    }
}

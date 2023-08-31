using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class APIManager : MonoBehaviour
{
    public static APIManager Instance;
    // Make sure to replace the token with your actual API token
    private string apiToken = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZGVudGlmaWVyIjoiTElBTTAxIiwidmVyc2lvbiI6InYyIiwicmVzZXRfZGF0ZSI6IjIwMjMtMDctMjkiLCJpYXQiOjE2OTA3NjUwMDgsInN1YiI6ImFnZW50LXRva2VuIn0.bc-L2YiMNmz9Gdr1DT4MjwIGtMiISBqjroYL9Xr69fWv8lVP8epLOEjs0UYfE8L9j9jTzcTekEVXcM8sAPnOBNBQC871e1soE9wOFuePVv9hmTH3r4B3mxQ7yHN2uYowvnEGjcn_1XsphnhX40pEsXBt7xdEfALXyCs_1r5y1nkcKfZSL7gmudDrAFXs8OkR1o077TYrU7jQLGtFSOZo0lOuWx8IgQArGejfJGsvNWYbqrqGRQcsg5wMCBQO1Rd-CNpWfGR-ey1gVew8fDUcJH01IV8IgB5dykt2wKhx7A2Bhp66yzXZGlW-1Bx-4isXQtHY2aQva_8fb77iuBo_uw";
    private string apiUrl = "https://api.spacetraders.io/v2/my/agent";
    public ApiData myData; 

    public bool isLoading = false;
    public UnityWebRequest.Result requestResult;
    
    [System.Serializable]
    public class PlayerData
    {
        public string accountId;
        public string symbol;
        public string headquarters;
        public int credits;
        public string startingFaction;
    }

    [System.Serializable]
    public class ApiData
    {
        public string msg;
        public string token;

    }

    [System.Serializable]
    public class DataWrapper
    {
        //public PlayerData data;
        public ApiData data;
    }

    public void Start()
    {
        MakeAPIRequest();
        Instance = this;

    }
    
    public void MakeAPIRequest()
    {
        //StartCoroutine(SendRequest());
        //StartCoroutine(RegisterPlayer());
    }

    private IEnumerator SendRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiToken);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            
            //dataWrapper = JsonUtility.FromJson<DataWrapper>(responseText);
            // Now you can access the properties of MyData through the DataWrapper class
            /*PlayerData myData = dataWrapper.data;
            Debug.Log("Account ID: " + myData.accountId);
            Debug.Log("Symbol: " + myData.symbol);
            Debug.Log("Headquarters: " + myData.headquarters);
            Debug.Log("Credits: " + myData.credits);
            Debug.Log("Starting Faction: " + myData.startingFaction);*/
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
 
    public IEnumerator PostRequest(string username, string password, string path)
    {
        //playerName = "Liam02";
        // Define the data to send in JSON format
        
        
        //string jsonData = "{\"symbol\": \"" + username + "\", \"faction\": \"COSMIC\"}";
        //string jsonData = $"{{\"username\": \"{username}\", \"password\": \"{password}\", \"role\": \"{"SUPER_ADMIN"}\"}}";
        string jsonData = $"{{\"username\": \"{username}\", \"password\": \"{password}\"}}";

        // Set up the request
        UnityWebRequest request = UnityWebRequest.Post($"http://studio6-api-host.op-bit.nz/api/v1/{path}", jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("Content-Type", "application/json");
        isLoading = true;
        // Send the request
        yield return request.SendWebRequest();
        isLoading = false;

        // Handle the response
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log(responseText);
            myData = JsonUtility.FromJson<ApiData>(responseText);
            //ApiData myData = dataWrapper.data;
            Debug.Log(myData.token);
            APIToken.token = myData.token;
            // You can process the responseText as needed
        }

        Debug.Log(request.result + ", " + path);
        
        if(request.result == UnityWebRequest.Result.Success && path == "auth/register"){
            StartCoroutine(PostRequest(username, password, "auth/login"));
        }

        if(request.result == UnityWebRequest.Result.Success && path == "auth/login"){
            SceneManager.LoadScene("CharacterCreate");
            
        }

        requestResult = request.result;

        request.Dispose();
    }
}

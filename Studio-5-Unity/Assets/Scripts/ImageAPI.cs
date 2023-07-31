using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ImageAPI : MonoBehaviour
{
    public RawImage rawImage;
    public TMP_InputField promptInput;

    // Replace this with your actual OpenAI API endpoint URL
    private string URL = "https://api.openai.com/v1/images/generations";

    // Replace "YOUR_OPENAI_API_KEY" with your actual OpenAI API key
    private string apiKey = "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";

    private void Start()
    {

    }

    private IEnumerator FetchImageCoroutine(string prompt)
    {
        // Create a JSON object for the API request
        string jsonRequest = $"{{ \"prompt\": \"{prompt}\", \"n\": 1, \"size\": \"1024x1024\" }}";

        // Set up the UnityWebRequest
        var request = new UnityWebRequest(URL, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerTexture();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error fetching image: " + request.error);
        }
        else
        {
            // Get the downloaded texture
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            // Assign the texture to the RawImage component
            rawImage.texture = texture;
        }
    }

    public void FetchImage()
    {
        string prompt = promptInput.text;
        StartCoroutine(FetchImageCoroutine(prompt));
    }
}

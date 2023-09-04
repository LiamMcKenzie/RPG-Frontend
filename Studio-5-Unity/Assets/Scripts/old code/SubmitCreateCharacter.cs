using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitCreateCharacter : MonoBehaviour
{
    public NewCharacter characterManager;
    public string name = "name";
    public string gender = "gender";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(characterManager.NewGetAllCharacters());
        StartCoroutine(characterManager.NewSeedBuild());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCharacter()
    {
        //Debug.Log(APIToken.token);
        StartCoroutine(characterManager.NewPostRequest());
        
    }
}

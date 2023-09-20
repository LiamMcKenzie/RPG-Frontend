using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitCreateCharacter : MonoBehaviour
{
    public TMP_InputField nameTMP;
    //public TMP_InputField genderTMP;
    public CharacterManager characterManager;
    public string name = "name";
    public string gender = "MALE";

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(characterManager.NewGetAllCharacters());
        //StartCoroutine(characterManager.NewSeedBuild());
    }

    // Update is called once per frame
    void Update()
    {
        name = nameTMP.text;
    }

    public void CreateCharacter()
    {
        //Debug.Log(APIToken.token);
        StartCoroutine(characterManager.NewPostRequest(name, gender));
        
        
    }
}

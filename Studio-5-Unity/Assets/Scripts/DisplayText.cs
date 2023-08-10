using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayText : MonoBehaviour
{
    public TMP_Text tmpText;
    public APIManager apiManager;

    public string playerName = "Liam02";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = apiManager.dataWrapper.data.symbol;
    }
}

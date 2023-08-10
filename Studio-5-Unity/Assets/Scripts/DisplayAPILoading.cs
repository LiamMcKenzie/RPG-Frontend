using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAPILoading : MonoBehaviour
{
    public APIManager apiManager;

    public GameObject loadingRenderer;
    // Start is called before the first frame update
    /*
    void Start()
    {
        apiManager = APITest.Instance;
    }*/

    // Update is called once per frame
    void Update()
    {
        loadingRenderer.SetActive(apiManager.isLoading);
    }
}

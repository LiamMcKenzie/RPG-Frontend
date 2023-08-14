using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectActive : MonoBehaviour
{
    public GameObject selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableObject ()
    {
        selectedObject.SetActive(false);
    }

    public void EnableObject ()
    {
        selectedObject.SetActive(true);
    }
}

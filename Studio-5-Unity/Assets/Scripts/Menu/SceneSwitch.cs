using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string SceneName; // Name of the scene

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision dected with player");
            Debug.Log("Collision detected with " + other.gameObject.name);
            
            SceneManager.LoadScene(SceneName);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject c = EventSystem.current.currentSelectedGameObject;
            if (c == null) { return; }
       
            Selectable s = c.GetComponent<Selectable>();
            if (s == null) { return; }
 
            Selectable jump = Input.GetKey(KeyCode.LeftShift)
                ? s.FindSelectableOnUp() : s.FindSelectableOnDown();
            if (jump != null) { jump.Select(); }
        }
    }
}

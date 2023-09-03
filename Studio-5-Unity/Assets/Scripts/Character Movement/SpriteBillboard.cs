using UnityEngine;
using Cinemachine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true; //Rotate sprites only around y-axis
    [SerializeField] CinemachineVirtualCamera vcam; //Attached Player Camera

    /// <summary>
    /// Rotates sprite to face camera. Main rotation around Y axis if checked (locks objects to ground)
    /// </summary>
    void Update()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, vcam.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = vcam.transform.rotation;
        }
    }
}

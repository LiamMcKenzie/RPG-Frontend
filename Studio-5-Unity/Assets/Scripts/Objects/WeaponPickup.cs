using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public ParticleSystem trails;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Run Function here to update player status (increase level, give player weapon)
            trails.Play();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum WeaponType
    {
        axe,
        bow,
        staff
    }
    
    public List <Mesh> weaponMeshes = new List<Mesh>();

    public MeshRenderer texture;
    public MeshFilter mesh;

    public int weaponLevel;
    public WeaponType weaponType;
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

    public void Update()
    {
        mesh.mesh = weaponMeshes[(int)weaponType];
    }
}

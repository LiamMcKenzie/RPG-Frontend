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
    public List <Material> axeMaterials = new List<Material>();
    public List <Color> tierColours = new List<Color>();

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
        trails.startColor = tierColours[weaponLevel];
        mesh.mesh = weaponMeshes[(int)weaponType];
        texture.material = axeMaterials[weaponLevel];
        
    }
}

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
    public List <Material> bowMaterials = new List<Material>();
    public List <Material> staffMaterials = new List<Material>();
    public List <Color> tierColours = new List<Color>();

    public MeshRenderer texture;
    public MeshFilter mesh;
    public Outline outline;
    public ParticleSystem trails;
    public ParticleSystem sparkles;


    public int weaponLevel;
    public WeaponType weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<EquipWeapon>().EquipWeaponCheck(texture, mesh);
            //Run Function here to update player status (increase level, give player weapon)
            trails.Play();
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        outline.OutlineColor = tierColours[weaponLevel];
        sparkles.startColor = tierColours[weaponLevel];
        trails.startColor = tierColours[weaponLevel];
        mesh.mesh = weaponMeshes[(int)weaponType];
        texture.material = axeMaterials[weaponLevel];

        switch ((int)weaponType)
        {
        case 0:
            texture.material = axeMaterials[weaponLevel];
            break;
        case 1:
            texture.material = bowMaterials[weaponLevel];
            break;
        case 2:
            texture.material = staffMaterials[weaponLevel];
            break;
        default:
            texture.material = axeMaterials[weaponLevel];
            break;
        }
        
    }
}

using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapons : MonoBehaviour
{
    public int WeaponsUnlocked = 1;

    // pseudo kode for weapon systemet: Der skal laves en switch for hvert eneste case, med hvilke våben der er unlocked.
    // der skal også laves en attack funktion.

    // Update is called once per frame
    void Update()
    {

       // if(Input.GetKeyDown(KeyCode.Alpha1))
       // {
       //     EquipWeapon(new MetalPipe());
       // }

       // if (Input.GetKeyDown(KeyCode.Alpha2)) 
       // {
       //     EquipWeapon(new )
       // }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Attack();
        }

    }
}

abstract class Weapon
{
    public string name;
    public int damage;
    public float range;
    public float AttackSpeed;
    public float Cooldown;
    //public bool isEquipped;
    public bool isRanged;
    public bool isMelee;

    public Weapon(string name, int damage, float range, float AttackSpeed, float Cooldown, bool isRanged, bool isMelee) //bool isEquipped
    {
        this.name = name;
        this.damage = damage;
        this.range = range;
        this.AttackSpeed = AttackSpeed;
        this.Cooldown = Cooldown;
        //this.isEquipped = false;
        this.isRanged = false;
        this.isMelee = false;

    }
    public void Attack()
    {
        Debug.Log("Attacking with " + name);
    }
}
   
class MetalPipe : Weapon
{
    
    public MetalPipe() : base("Metal Pipe", 10, 1.5f, 15f, 1f, false, true)
    {

    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapons : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }
}

abstract class Weapon
{
    public string name;
    public int damage;
    public float range;
    public float AttackSpeed;
    public float Cooldown;
    public bool isEquipped;
    public Weapon(string name, int damage, float range, float AttackSpeed, float Cooldown, bool isEquipped)
    {
        this.name = name;
        this.damage = damage;
        this.range = range;
        this.AttackSpeed = AttackSpeed;
        this.Cooldown = Cooldown;
        this.isEquipped = false;
    }
        //public virtual void Attack()
       // {
           // Debug.Log("Attacking with " + name);
       // }
}
   
class MetalPipe : Weapon
{
    name = "Metal Pipe";
        damage = 10;
        range = 1.5f;
        AttackSpeed = 15f;
        Cooldown = 1f;
        isEquipped = false;

}
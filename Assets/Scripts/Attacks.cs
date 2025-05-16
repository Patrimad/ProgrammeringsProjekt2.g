using UnityEngine;

public class Attacks : MonoBehaviour
{
    public abstract class Attack
    {
        protected int damage;
        protected float duration;
        protected float cooldown;

        public abstract void UseAttack();

        protected void StartCooldown()
        {
            Debug.Log("Starting cooldown for: " + cooldown + " seconds.");
        }
    }

    // Derived melee class
    public class Melee : Attack
    {
        public override void UseAttack()
        {
            Debug.Log("Using melee attack.");
            StartCooldown();
        }
    }

    // Specific melee attack
    public class SpikeHand : Melee
    {
        public override void UseAttack()
        {
            Debug.Log("Using spike hand attack!");
            base.UseAttack();
        }
    }

    // Derived ranged class
    public class Ranged : Attack
    {
        public override void UseAttack()
        {
            Debug.Log("Using ranged attack.");
            StartCooldown();
        }
    }

    // Specific ranged attack
    public class Spikes : Ranged
    {
        public override void UseAttack()
        {
            Debug.Log("Firing spikes!");
            base.UseAttack();
        }
    }

    // Example usage in Unity
    private void Start()
    {
        Attack attack = new SpikeHand();
        attack.UseAttack();

        attack = new Spikes();
        attack.UseAttack();
    }
}
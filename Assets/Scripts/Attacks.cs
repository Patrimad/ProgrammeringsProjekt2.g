using UnityEngine;

public class Attacks : MonoBehaviour
{
    public GameObject knifePrefab;
    public GameObject circlePrefab;

    public bool isSecondPlayer = false; // NEW: Second player toggle

    private GameObject activeKnife;

    public Health_Stam stats;

    public int attack1Cost = 5;
    public int attack2Cost = 10;

    void Update()
    {
        if (isSecondPlayer)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Attack attack = new SpikeHand(knifePrefab, transform, this);
                attack.UseAttack();
                stats.TakeBlood(attack1Cost);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Attack attack = new Spikes(circlePrefab, transform);
                attack.UseAttack();
                stats.TakeBlood(attack2Cost);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Attack attack = new SpikeHand(knifePrefab, transform, this);
                attack.UseAttack();
                stats.TakeBlood(attack1Cost);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Attack attack = new Spikes(circlePrefab, transform);
                attack.UseAttack();
                stats.TakeBlood(attack2Cost);
            }
        }

        if (activeKnife != null)
        {
            activeKnife.transform.position = transform.position;
            activeKnife.transform.rotation = transform.rotation;
        }
    }

    public void SetActiveKnife(GameObject knife)
    {
        if (activeKnife != null)
        {
            Destroy(activeKnife);
        }
        activeKnife = knife;
    }

    public abstract class Attack
    {
        protected int damage;
        protected float duration;
        protected float cooldown;

        public abstract void UseAttack();

        protected void StartCooldown()
        {
        }
    }

    public class Melee : Attack
    {
        public override void UseAttack()
        {
            StartCooldown();
        }
    }

    public class SpikeHand : Melee
    {
        private GameObject knifePrefab;
        private Transform playerTransform;
        private Attacks attackController;

        public SpikeHand(GameObject prefab, Transform transform, Attacks controller)
        {
            knifePrefab = prefab;
            playerTransform = transform;
            attackController = controller;
        }

        public override void UseAttack()
        {
            GameObject knife = Object.Instantiate(knifePrefab, playerTransform.position, playerTransform.rotation);
            attackController.SetActiveKnife(knife);
            base.UseAttack();
        }
    }

    public class Ranged : Attack
    {
        public override void UseAttack()
        {
            StartCooldown();
        }
    }

    public class Spikes : Ranged
    {
        private GameObject circlePrefab;
        private Transform playerTransform;

        public Spikes(GameObject prefab, Transform transform)
        {
            circlePrefab = prefab;
            playerTransform = transform;
        }

        public override void UseAttack()
        {
            Object.Instantiate(circlePrefab, playerTransform.position, Quaternion.identity);
            base.UseAttack();
        }
    }
}

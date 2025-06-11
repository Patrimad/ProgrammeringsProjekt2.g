using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    public GameObject knifePrefab;
    private GameObject activeKnife;

    public Health_Stam stats;

    public float attackCooldown = 3.0f;
    private float attackTimer = 0f;

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            Attack attack = new SpikeHand(knifePrefab, transform, this);
            attack.UseAttack();

            attackTimer = 0f;
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

        protected void StartCooldown() { }
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
        private EnemyAttacks attackController;

        public SpikeHand(GameObject prefab, Transform transform, EnemyAttacks controller)
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
}

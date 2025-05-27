using UnityEngine;

public class PointDamage : MonoBehaviour
{
    public int damageAmount = 20;
    public LayerMask enemyLayers;
    public float lifeTime = 2f;
    public float radius = 3f;
    public float angle = 45f; // degrees

    void Start()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Vector3 directionToTarget = (enemy.transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

            if (angleToTarget <= angle / 2f)
            {
                IDamage damageable = enemy.GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                }
            }
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Optional: visualize the 45-degree arc in the scene
        Vector3 forward = transform.forward * radius;
        Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, angle / 2f, 0) * forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
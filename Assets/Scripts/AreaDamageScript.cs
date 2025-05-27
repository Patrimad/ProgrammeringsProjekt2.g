using Unity.VisualScripting;
using UnityEngine;

public class AreaDamageScript : MonoBehaviour
{
    public int damageAmount = 20;
    public LayerMask enemyLayers;
    public float lifeTime = 2f;
    public float radius = 3f;

    void Start()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, radius, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            IDamage damageable = enemy.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }
        
        Destroy(gameObject, lifeTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
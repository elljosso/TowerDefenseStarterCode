using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float attackRange = 3f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 
    public float projectileSpeed = 1f; // Speed of the projectile
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public TowerType type; // the type of this tower 

    private float attackTimer; // Timer to keep track of attack rate

    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        // Increment the attack timer
        attackTimer += Time.deltaTime;

        // Check if it's time to attack
        if (attackTimer >= 1f / attackRate)
        {
            // Reset attack timer
            attackTimer = 0f;

            // Find all enemies within attack range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Shoot at the enemy
                    Shoot(collider.gameObject);
                    break; // Only shoot at the first enemy found
                }
            }
        }
    }

    void Shoot(GameObject enemy)
    {
        // Instantiate the bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set the bullet properties
        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = enemy.transform;
            projectile.speed = projectileSpeed; // Set the speed of the projectile
            bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);
        }
    }
}
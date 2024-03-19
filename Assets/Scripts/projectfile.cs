using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        // Draai het projectiel naar het doelwit
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Update()
    {
        // Wanneer het doelwit null is, bestaat het niet meer en moet dit object worden verwijderd
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        // Verplaats het projectiel naar het doelwit
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Controleer of de afstand tussen dit object en het doelwit kleiner is dan 0.2.
        // Zo ja, vernietig dit object.
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            target.GetComponent<UFO>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
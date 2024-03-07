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
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, 0f);
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
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Controleer of de afstand tussen dit object en het doelwit kleiner is dan 0.2.
        // Zo ja, vernietig dit object.
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
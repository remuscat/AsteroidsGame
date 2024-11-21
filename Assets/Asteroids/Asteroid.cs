using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 50f;
    public Vector2 direction;
    private float lifetime = 10f; // Time after which asteroid will be destroyed

    void Start()
    {
        // Randomize the direction of the asteroid's movement
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Destroy(gameObject, lifetime);  // Destroy after a certain time
    }

    void Update()
    {
        // Move the asteroid in the specified direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Rotate the asteroid for a more natural look
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}

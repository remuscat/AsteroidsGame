using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 5f; // Base speed of the asteroid
    public float health;     // Asteroid health, set based on size
    private Vector2 direction;
    public GameObject explosionPrefab;  // Reference to the explosion prefab
    public Vector3 explosionScale;      // Scale of the explosion, set by the spawner

    void Start()
    {
        // Ensure the asteroid always moves towards the left
        direction = Vector2.left;

        // Optional: Add some randomization to the vertical movement
        float verticalOffset = Random.Range(-0.5f, 0.5f); // Slight vertical deviation
        direction += new Vector2(0f, verticalOffset).normalized;

        // Set health based on the asteroid's size (explosionScale.x is a good reference)
        health = explosionScale.x * 10f; // Example: Larger asteroids have more health
    }

    void Update()
    {
        // Move the asteroid in the set direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Get the screen boundaries
        float screenLeftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x - 1f;
        float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x + 1f;
        float screenTopEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y + 1f;  // Top boundary
        float screenBottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y - 1f;  // Bottom boundary

        // Debug logs to check boundaries
        // Debug.Log("screenLeftEdge: " + screenLeftEdge + " transform.position.x:" + transform.position.x);
        // Debug.Log("screenRightEdge: " + screenRightEdge + " transform.position.x:" + transform.position.y);

        // Destroy the asteroid if it moves completely off-screen to the left, right, top, or bottom
        if (transform.position.x < screenLeftEdge || transform.position.x > screenRightEdge ||
            transform.position.y < screenBottomEdge || transform.position.y > screenTopEdge)
        {
            Destroy(gameObject);
        }
    }

    // Handle collision with the spaceship or bullet
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spaceship"))
        {
            // Handle spaceship collision (e.g., game over)
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = explosionScale;

            Destroy(gameObject);
            // Optionally, destroy the spaceship or trigger a game over logic
            Debug.Log("Asteroid hit the spaceship!");
            
            // Destroy(other.gameObject); // Destroy the spaceship
        }
        else if (other.CompareTag("Bullet"))
        {
            // Get the bullet's size to determine the damage it deals
            BulletMovement bullet = other.GetComponent<BulletMovement>();
            if (bullet != null)
            {
                // Calculate damage based on bullet size
                float damage = bullet.GetBulletSize() * 5f; // Damage scales with bullet size
                health -= damage; // Reduce asteroid health

                // Instantiate explosion effect based on asteroid size
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.transform.localScale = explosionScale;

                // Destroy the bullet
                Destroy(other.gameObject);

                Debug.Log("health:"+health + " <> "+ bullet.GetBulletSize());

                // If the asteroid's health is 0 or below, destroy it
                if (health <= 0)
                {
                    DestroyAsteroid();
                }
            }
        }
    }
    // Destroy the asteroid and trigger an explosion
    private void DestroyAsteroid()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = explosionScale;

        Destroy(gameObject); // Destroy the asteroid
    }
}

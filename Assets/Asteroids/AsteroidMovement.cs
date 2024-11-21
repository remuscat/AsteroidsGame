using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 5f; // Base speed of the asteroid
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
    // Handle collision with the spaceship
    void OnTriggerEnter2D(Collider2D  other)
    {
        if (other.CompareTag("Spaceship"))
        {
            // You can add what happens when the asteroid collides with the spaceship, e.g., destroy the asteroid
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = explosionScale;

            Destroy(gameObject);
            // Optionally, destroy the spaceship or trigger a game over logic
            Debug.Log("Asteroid hit the spaceship!");
        }
    }
}

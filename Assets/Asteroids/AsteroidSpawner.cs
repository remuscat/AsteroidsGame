using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Drag your asteroid prefabs here
    // public float spawnInterval = 2f;    // Time between spawns
    public float spawnYRange = 5f;      // Vertical range for spawning
    public float minSize = 1f;          // Minimum asteroid size
    public float maxSize = 2f;          // Maximum asteroid size
    public float speed = 5f;            // Asteroid movement speed

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnAsteroid();
            float spawnInterval=Random.Range(0.0f, 5.0f);
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

        void SpawnAsteroid()
    {
        // Pick a random asteroid prefab
        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject asteroid = Instantiate(asteroidPrefabs[randomIndex]);

        
        // Get screen bounds in world space
        Camera mainCamera = Camera.main;
        Vector3 screenRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f)); // Right edge
        Vector3 screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));       // Top edge
        Vector3 screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));    // Bottom edge

        // Set spawn position
        float spawnX = screenRightEdge.x + 1f; // Just off-screen on the right
        float spawnY = Random.Range(screenBottom.y, screenTop.y); // Random Y within screen bounds
        asteroid.transform.position = new Vector3(spawnX, spawnY, 0f);

        // Set random size
        float randomSize = Random.Range(minSize, maxSize);
        asteroid.transform.localScale = new Vector3(randomSize, randomSize, 1f);

        // Set random rotation
        float randomRotation = Random.Range(0f, 360f);
        asteroid.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);

        // Add movement script
        AsteroidMovement movement = asteroid.AddComponent<AsteroidMovement>();
        movement.speed = speed;
    }

}

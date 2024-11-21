using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the ship
    public GameObject bulletPrefab;      // Drag your bullet prefab here
    public Transform bulletSpawnPoint;   // The point from which bullets are fired
    public float fireRate = 0.5f;        // Time between shots

    private bool isSpacePressed = false;    // To track if space is being held down
    private float spaceHeldTime = 0f;       // Time spacebar is being held
    private GameObject currentBullet;       // Store the current bullet instance

    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        // Ensure the main camera is assigned properly
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Please ensure there is a camera tagged as 'MainCamera' in the scene.");
        }
    }

    void Update()
    {
        if (mainCamera == null) return; // Prevent further execution if mainCamera is null

        // Get input from arrow keys
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right
        float vertical = Input.GetAxis("Vertical");     // Up/Down

        // Calculate movement
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // Apply movement
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Clamp the position based on the camera's viewport
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Clamp to the left, right, top, and bottom of the screen
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0.016f, 0.98f);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0.03f, 0.97f);

        // Convert the clamped position back to world space
        transform.position = mainCamera.ViewportToWorldPoint(screenPosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Keep Z-axis unchanged

        // Spacebar being pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isSpacePressed)
        {
            // Start tracking the spacebar hold time when it's pressed
            spaceHeldTime = Time.time;
            isSpacePressed = true; // Mark that space was pressed

            // Instantiate the bullet immediately when space is pressed
            currentBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // Pass the initial bullet size (1) to start with
            currentBullet.GetComponent<BulletMovement>().SetBulletSize(0f);
        }

        // Spacebar being held down (while it's pressed)
        if (Input.GetKey(KeyCode.Space) && isSpacePressed)
        {
            if (currentBullet != null) // Ensure the bullet exists
            {
                // Update spaceHeldTime while the key is held
                float currentHoldTime = Time.time - spaceHeldTime;

                // Update the bullet size and move it forward as the spacebar is held
                currentBullet.GetComponent<BulletMovement>().SetBulletSize(currentHoldTime);
                
                // Keep the bullet in front of the ship (follow the ship's position)
                currentBullet.transform.position = bulletSpawnPoint.position;
            }
            else
            {
                // If the bullet is destroyed, instantiate a new one
                currentBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
                currentBullet.GetComponent<BulletMovement>().SetBulletSize(0f); // Reset size
            }
        }

        // Spacebar being released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isSpacePressed = false; // Reset flag when space is released
        }
    }
}

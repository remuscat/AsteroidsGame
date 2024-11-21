using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;
    private float sizeMultiplier = 1f;  // Default size multiplier

    void Update()
    {
        // Move the bullet along the x-axis (sideways)
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy the bullet after it moves out of the screen (adjust these values as needed)
        if (transform.position.x > 20f || transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void SetBulletSize(float spaceHeldTime)
    {
        // Map spaceHeldTime to a size multiplier
        // If spaceHeldTime is between 1 and 3 seconds, increase bullet size linearly
        float maxSize = 5f;
        float minSize = 0.5f;
        float sizeFactor = Mathf.InverseLerp(0.5f, 3f, spaceHeldTime); // Returns a value between 0.5 and 3

        // Calculate the bullet size
        sizeMultiplier = Mathf.Lerp(minSize, maxSize, sizeFactor);

        // Apply the new size to the bullet
        transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, 1f);
    }
}

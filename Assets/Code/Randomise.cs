using UnityEngine;

public class RandomizeTransform : MonoBehaviour
{
    [Header("Randomization Settings")]
    [Tooltip("Max amount to randomize position (in units)")]
    public float positionRandomization = 0.1f; // Adjustable position randomization
    [Tooltip("Max amount to randomize rotation (in degrees)")]
    public float rotationRandomization = 5f; // Adjustable rotation randomization

    private void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        // Randomize position
        Vector3 randomPositionOffset = new Vector3(
            Random.Range(-positionRandomization, positionRandomization),
            Random.Range(-positionRandomization, positionRandomization),
            0 // No change in Z-axis for 2D
        );

        transform.position += randomPositionOffset; // Apply random position offset

        // Randomize rotation (only around Z-axis in 2D)
        float randomRotationOffset = Random.Range(-rotationRandomization, rotationRandomization);
        transform.eulerAngles += new Vector3(0, 0, randomRotationOffset); // Apply random rotation offset
    }

    // Optional: Uncomment to randomize every few seconds
    /*
    private void Update()
    {
        if (Time.frameCount % 60 == 0) // Randomize every second (assuming 60 FPS)
        {
            Randomize();
        }
    }
    */
}

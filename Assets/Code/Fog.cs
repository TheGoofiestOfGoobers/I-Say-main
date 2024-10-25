using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [Header("Sprite Settings")]
    [SerializeField] private SpriteRenderer targetRenderer; // The SpriteRenderer to display the sprites
    [SerializeField] private Sprite[] sprites; // Array of sprites to swap between

    [Header("Swap Settings")]
    [SerializeField] private float swapSpeed = 1f; // Time (in seconds) between sprite swaps
    [SerializeField, Range(0f, 1f)] private float opacity = 1f; // Opacity of the sprite (0 is transparent, 1 is fully opaque)

    private int currentSpriteIndex = 0; // Index of the current sprite being displayed
    private float timer = 0f; // Timer to keep track of when to swap

    void Start()
    {
        if (targetRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is not assigned!");
            return;
        }

        if (sprites.Length == 0)
        {
            Debug.LogError("No sprites assigned!");
            return;
        }

        // Set the initial sprite and opacity
        UpdateSprite();
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // If the timer exceeds the swap speed, swap the sprite
        if (timer >= swapSpeed)
        {
            SwapSprite();
            timer = 0f; // Reset the timer
        }

        // Update the opacity of the sprite
        UpdateOpacity();
    }

    private void SwapSprite()
    {
        // Increment the index to the next sprite, looping back to 0 if necessary
        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;

        // Update the sprite and opacity
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        // Set the new sprite
        targetRenderer.sprite = sprites[currentSpriteIndex];
        UpdateOpacity();
    }

    private void UpdateOpacity()
    {
        // Update the color of the sprite to reflect the new opacity
        Color color = targetRenderer.color;
        color.a = opacity;
        targetRenderer.color = color;
    }
}

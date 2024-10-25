using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays : MonoBehaviour
{
    public GameObject[] keys;  // Assign the piano key GameObjects in the inspector
    public AudioClip[] keySounds;  // Assign corresponding audio clips for each key
    public Sprite[] defaultSprites;  // Assign the default (unlit) sprites in the inspector
    public Sprite[] highlightedSprites;  // Assign the highlighted (lit) sprites in the inspector

    // New variable for volume control
    [Range(0f, 1f)] // This will create a slider in the inspector
    public float volume = 1.0f; // Default volume level (max)

    private List<int> sequence = new List<int>();  // Stores the sequence of keys
    private int playerIndex = 0;
    private bool playerTurn = false;

    void Start()
    {
        StartNewGame();  // Begin the first round
    }

    void StartNewGame()
    {
        sequence.Clear();
        AddToSequence();
        StartCoroutine(PlaySequence());
    }

    void AddToSequence()
    {
        int randomKey = Random.Range(0, keys.Length);  // Choose a random key
        sequence.Add(randomKey);  // Add it to the sequence
    }

    IEnumerator PlaySequence()
    {
        playerTurn = false;  // Disable player input during the sequence
        yield return new WaitForSeconds(1.5f);  // Add a small delay before the first key

        for (int i = 0; i < sequence.Count; i++)
        {
            int keyIndex = sequence[i];
            HighlightKey(keyIndex);  // Visual feedback (switch to highlighted sprite)
            PlayKeySound(keyIndex);  // Play the haunting melody note
            yield return new WaitForSeconds(1.0f);  // Wait between notes
            ResetKeyVisuals(keyIndex);  // Switch back to default sprite
            yield return new WaitForSeconds(0.5f);  // Add a delay between keys
        }

        playerTurn = true;  // Enable player input after the sequence finishes
        playerIndex = 0;  // Reset player index for the new round
    }

    void HighlightKey(int index)
    {
        // Switch to the highlighted sprite for the key
        keys[index].GetComponent<SpriteRenderer>().sprite = highlightedSprites[index];
    }

    void ResetKeyVisuals(int index)
    {
        // Switch back to the default sprite
        keys[index].GetComponent<SpriteRenderer>().sprite = defaultSprites[index];
    }

    void PlayKeySound(int index)
    {
        // Play sound associated with the key with volume adjustment
        AudioSource.PlayClipAtPoint(keySounds[index], keys[index].transform.position, volume);
    }

    void Update()
    {
        if (playerTurn && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedKey = hit.collider.gameObject;
                int keyIndex = System.Array.IndexOf(keys, clickedKey);

                if (keyIndex == sequence[playerIndex])
                {
                    HighlightKey(keyIndex);
                    PlayKeySound(keyIndex);
                    playerIndex++;

                    // Start a coroutine to reset the key after a short delay
                    StartCoroutine(ResetKeyAfterDelay(keyIndex, 1.0f));

                    if (playerIndex >= sequence.Count)
                    {
                        // Player successfully completed the sequence
                        AddToSequence();
                        StartCoroutine(PlaySequence());
                    }
                }
                else
                {
                    // Player made a mistake
                    StartCoroutine(HandleFailure());
                }
            }
        }
    }

    IEnumerator ResetKeyAfterDelay(int keyIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetKeyVisuals(keyIndex);  // Switch back to default sprite
    }

    IEnumerator HandleFailure()
    {
        playerTurn = false;
        // Show spooky visual/sound effects for failure
        Debug.Log("You failed! Spooky stuff happens...");
        yield return new WaitForSeconds(2.0f);  // Wait before restarting
        StartNewGame();  // Restart the game
    }
}

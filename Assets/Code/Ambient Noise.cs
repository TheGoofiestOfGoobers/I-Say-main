using UnityEngine;

public class AmbientSoundPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] ambientSounds; // Array for the ambient sounds
    [SerializeField] private float volume = 2.0f; // Volume of the played sound

    [Header("Time Settings")]
    [SerializeField] private float minTimeBetweenSounds = 5f; // Minimum time between sounds
    [SerializeField] private float maxTimeBetweenSounds = 15f; // Maximum time between sounds

    private float timeToNextSound; // Time remaining until the next sound is played

    void Start()
    {
        if (ambientSounds.Length == 0)
        {
            Debug.LogError("No ambient sounds assigned!");
            return;
        }

        ScheduleNextSound(); // Schedule the first sound
    }

    void Update()
    {
        if (timeToNextSound > 0)
        {
            timeToNextSound -= Time.deltaTime;
            if (timeToNextSound <= 0)
            {
                PlayRandomSound();
                ScheduleNextSound();
            }
        }
    }

    private void PlayRandomSound()
    {
        if (ambientSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, ambientSounds.Length);
            // Play the sound at the position of the AmbientSoundPlayer GameObject
            AudioSource.PlayClipAtPoint(ambientSounds[randomIndex], transform.position, volume);
        }
    }

    private void ScheduleNextSound()
    {
        timeToNextSound = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }
}

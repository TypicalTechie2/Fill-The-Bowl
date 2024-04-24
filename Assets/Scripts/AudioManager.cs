using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    // Ensure only one instance of AudioManager exists
    void Awake()
    {
        // If an instance already exists, destroy this object
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this instance as the singleton instance
        instance = this;

        // Ensure this GameObject persists across scene changes
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // Ensure the background music loops
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}

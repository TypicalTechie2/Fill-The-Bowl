using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button musicButton;
    [SerializeField] Sprite musicOnSprite;
    [SerializeField] Sprite musicOffSprite;
    [SerializeField] Image musicImage;
    public float buttonZoomAmount = 10f;
    public float buttonZoomSpeed = 2f;
    private Vector2 buttonInitialScale;
    public bool isMusicOn = true;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        buttonInitialScale = playButton.transform.localScale;

        // Get the Image component from the Source Image component of the music button
        musicImage = GameObject.Find("Music Button").GetComponent<Image>();
        // Set the initial sprite based on the initial state of the music
        musicImage.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomInAndOutPlayButton(); // Perform zoom in and out animation for the play button
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    // Method to perform zoom in and out animation for the play button
    void ZoomInAndOutPlayButton()
    {
        float playButtonScale = Mathf.Sin(Time.time * buttonZoomSpeed) * buttonZoomAmount;
        playButton.transform.localScale = buttonInitialScale + new Vector2(playButtonScale, playButtonScale);
    }

    // Method to toggle music on or off
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;

        // Check if the AudioManager instance exists
        if (AudioManager.instance != null && AudioManager.instance.audioSource != null)
        {
            if (isMusicOn)
            {
                AudioManager.instance.audioSource.UnPause();
                musicImage.sprite = musicOnSprite;
                // TODO: Turn on the music playback system
            }
            else
            {
                AudioManager.instance.audioSource.Pause();
                musicImage.sprite = musicOffSprite;
                // TODO: Turn off the music playback system
            }
        }
    }

    // Method to exit the game
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();

#endif
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject spawnArea;
    [SerializeField] TMP_Text clickHereText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text congratsText;
    [SerializeField] Button restartButton;
    [SerializeField] AudioSource gameAudio;
    [SerializeField] AudioClip roundWonSound;
    [SerializeField] AudioClip ballPopSound;
    private float spawnForce = 1f;
    public int maxBallCount = 100;
    public int currentBallCount = 0;
    public bool isGameActive = true;
    public bool soundClipPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelText(); // Update the level text when the game starts
        StartCoroutine(blinkClickHereText()); // Start the coroutine to blink the "Click Here" text
    }

    // Update is called once per frame
    void Update()
    {
        BallInstantiate(); // Check for input to spawn a ball
    }

    // Method to instantiate a ball when the player clicks
    void BallInstantiate()
    {
        if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isGameActive)
        {
            Vector2 inputPos = Input.mousePosition;

            if (Input.touchCount > 0)
            {
                inputPos = Input.GetTouch(0).position;
            }

            Vector2 rayPos = Camera.main.ScreenToWorldPoint(inputPos);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == spawnArea)
            {
                if (currentBallCount < maxBallCount)
                {
                    if (!IsSpawnPointOccupied(rayPos))
                    {
                        InstantiateBall(rayPos); // Instantiate a ball at the specified position
                        gameAudio.PlayOneShot(ballPopSound, 0.5f);
                    }
                }
            }
        }
    }

    // Method to check if the spawn point is occupied by another ball
    bool IsSpawnPointOccupied(Vector2 rayPos)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(rayPos);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ball"))
            {
                return true;
            }
        }
        return false;
    }

    // Method to instantiate a ball at the specified position
    public void InstantiateBall(Vector2 position)
    {
        GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;
            rb.AddForce(forceDirection * spawnForce, ForceMode2D.Impulse);
        }
        currentBallCount++;
    }

    // Coroutine to blink the "Click Here" text
    IEnumerator blinkClickHereText()
    {
        isGameActive = true;
        int blinkCount = 3;

        for (int i = 0; i < blinkCount; i++)
        {
            yield return new WaitForSeconds(1);
            clickHereText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            clickHereText.gameObject.SetActive(false);
        }
        clickHereText.gameObject.SetActive(false);
    }

    // Method to clear all spawned balls from the scene
    void ClearSpawnedBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        currentBallCount = 0;
    }

    // Method to handle storage full
    public void HandleStorageFull()
    {
        StartCoroutine(MoveToNextLevelRoutine()); // Start the coroutine to move to the next level
    }

    // Method to update the level text based on the current level
    void UpdateLevelText()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Level: " + currentLevel;
    }

    // Coroutine to move to the next level
    IEnumerator MoveToNextLevelRoutine()
    {
        if (!soundClipPlayed)
        {
            gameAudio.PlayOneShot(roundWonSound);
            soundClipPlayed = true;
        }
        gameAudio.PlayOneShot(roundWonSound);
        yield return new WaitForSeconds(3f);
        ClearSpawnedBalls();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalLevels = SceneManager.sceneCountInBuildSettings - 1;



        if (currentSceneIndex < totalLevels)
        {
            int nextLevel = currentSceneIndex + 1;
            SceneManager.LoadScene(nextLevel); // Load the next level(Scene)
            UpdateLevelText();
        }

        else
        {
            congratsText.gameObject.SetActive(true);
            isGameActive = false;
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);

            Debug.Log("Congratulations! You have cleared all the levels!");
        }
    }

    // Method to restart the game
    public void RestartGame()
    {
        ClearSpawnedBalls();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to return to the main menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Method to handle game over
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log("Game Over!");
    }
}

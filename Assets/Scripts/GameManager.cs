using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Button restartButton;
    [SerializeField] AudioSource gameAudio;
    [SerializeField] AudioClip roundWonSound;
    private float spawnForce = 1f;
    public int maxBallCount = 100;
    public int currentBallCount = 0;
    public bool isGameActive = true;
    public bool isGameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blinkClickHereText());
    }

    // Update is called once per frame
    void Update()
    {
        BallInstantiate();
    }

    void BallInstantiate()
    {
        if (Input.GetMouseButton(0) && isGameActive)
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == spawnArea)
            {
                if (currentBallCount < maxBallCount)
                {
                    if (!IsSpawnPointOccupied(rayPos))
                    {
                        InstantiateBall(rayPos);
                    }
                }
            }
        }
    }

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
        StartCoroutine(MoveToNextLevelRoutine());
    }

    IEnumerator MoveToNextLevelRoutine()
    {
        gameAudio.PlayOneShot(roundWonSound);
        yield return new WaitForSeconds(3f);
        ClearSpawnedBalls();
        // Go to next level or perform any other action
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        ClearSpawnedBalls();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        isGameActive = false;
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Debug.Log("Game Over!");
    }
}

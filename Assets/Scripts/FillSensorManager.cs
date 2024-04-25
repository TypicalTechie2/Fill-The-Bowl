using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillSensorManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] TMP_Text filledPercentageText; // Text displaying the filled percentage
    [SerializeField] TMP_Text stageClearText;   // Text displayed when the stage is cleared
    private int currentBallCount = 0;   // Current count of balls inside the fill sensor
    private int totalFilledPercentage;
    public bool storageIsFull = false;
    private Coroutine winConfirmationCoroutine;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            currentBallCount++;
            totalFilledPercentage = currentBallCount;
            filledPercentageText.text = totalFilledPercentage + "%";

            if (totalFilledPercentage == 100)
            {
                storageIsFull = true;

                // Start a coroutine to confirm win after a delay
                winConfirmationCoroutine = StartCoroutine(ConfirmWinAfterDelay());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            currentBallCount--;
            totalFilledPercentage = currentBallCount;
            filledPercentageText.text = totalFilledPercentage + "%";

            // If the number of balls is less than 100, cancel the win confirmation coroutine
            if (totalFilledPercentage < 100 && winConfirmationCoroutine != null)
            {
                StopCoroutine(winConfirmationCoroutine);
            }
        }
    }

    private IEnumerator ConfirmWinAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 3 seconds

        // If the container is still full after the delay, trigger win
        if (storageIsFull)
        {
            gameManager.HandleStorageFull();

            // Display stage clear text if the scene is not the last scene
            if (SceneManager.GetActiveScene().buildIndex != 5)
            {
                stageClearText.gameObject.SetActive(true);
            }
        }
    }
}

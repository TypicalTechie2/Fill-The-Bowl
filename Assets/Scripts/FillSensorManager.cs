using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillSensorManager : MonoBehaviour
{
    [SerializeField] TMP_Text filledPercentageText; // Text displaying the filled percentage
    [SerializeField] TMP_Text stageClearText;   // Text displayed when the stage is cleared
    private int currentBallCount = 0;   // Current count of balls inside the fill sensor
    private int totalFilledPercentage;
    public bool storageIsFull = false;
    public GameManager gameManager;

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

                // Display stage clear text if the scene is not the last scene
                if (SceneManager.GetActiveScene().buildIndex != 5)
                {
                    stageClearText.gameObject.SetActive(true);
                }

                gameManager.HandleStorageFull();
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
        }
    }
}

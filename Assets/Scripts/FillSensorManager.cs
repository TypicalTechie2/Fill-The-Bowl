using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillSensorManager : MonoBehaviour
{
    [SerializeField] TMP_Text filledPercentageText;
    [SerializeField] TMP_Text stageClearText;
    private int currentBallCount = 0;
    private int totalFilledPercentage;
    public bool storageIsFull = false;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                stageClearText.gameObject.SetActive(true);
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

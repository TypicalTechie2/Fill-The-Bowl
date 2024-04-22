using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject spawnArea;
    private int maxBallCount = 100;
    private int currentBallCount = 0;
    public bool isGameActive = true;
    public bool isGameOver = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BallInstantiate();
    }

    void BallInstantiate()
    {
        // Check for mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            // Cast a ray from the mouse position to detect if it hits the Spawn Area object
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null)
            {
                // Check if the ray hit the Spawn Area object
                if (hit.collider.gameObject == spawnArea)
                {
                    // Check if the current ball count is less than the maximum allowed
                    if (currentBallCount < maxBallCount)
                    {
                        // Instantiate a new ball at the spawn point
                        Instantiate(ballPrefab, rayPos, Quaternion.identity);
                        // Increment the current ball count
                        currentBallCount++;
                    }
                }
            }
        }
    }

}

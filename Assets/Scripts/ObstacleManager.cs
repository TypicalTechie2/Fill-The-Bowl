using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    private float ballAmplitudeX = 3f; // Adjust the size of the loop in X direction
    private float ballFrequencyX = 2f; // Adjust the speed of the loop in X direction
    private float ballAmplitudeY = 1.5f; // Adjust the size of the loop in Y direction
    private float ballFrequencyY = 1f; // Adjust the speed of the loop in Y direction
    private float wallRotationSpeed = 25f; // Adjust this to change rotation speed
    private float fireBallMoveSpeed = 100f;
    private float fireBallZoomSpeed = 1f;
    private float fireBallZoomAmount = 2.5f;
    private Vector2 fireBallInitialScale;
    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBallObstacle();
        WallObstacleRotate();
        FireBallZoonInAndOUt();
    }

    void MoveBallObstacle()
    {
        if (obstacle == obstacle.gameObject.CompareTag("BallObstacle") && gameManager.isGameActive)
        {
            // Calculate the X and Y coordinates based on time
            float x = Mathf.Sin(Time.time * ballFrequencyX) * ballAmplitudeX;
            float y = Mathf.Cos(Time.time * ballFrequencyY) * ballAmplitudeY;

            // Create a new position vector
            Vector2 newPosition = new Vector3(x, y);

            // Update the position of the object
            obstacle.transform.position = newPosition;

            // Move the object horizontally
            obstacle.transform.Translate(Vector2.right * Time.deltaTime);
        }

    }

    //Rotate's the wall obstacle object in set direction
    void WallObstacleRotate()
    {

        if (obstacle == obstacle.gameObject.CompareTag("WallObstacle") && gameManager.isGameActive)
        {
            obstacle.transform.Rotate(Vector3.forward * wallRotationSpeed * Time.deltaTime);
        }
    }

    //Performs a zoom in and out animation for a fireball obstacle if it meets certain conditions.
    void FireBallZoonInAndOUt()
    {
        // Check if the obstacle is a fireball and the game is active
        if (obstacle == obstacle.gameObject.CompareTag("FireBall") && gameManager.isGameActive)
        {
            // Rotate the fireball
            obstacle.transform.Rotate(Vector3.back * fireBallMoveSpeed * Time.deltaTime);

            // Calculate scale for zoom in and out animation
            float fireBallScale = Mathf.Sin(Time.time * fireBallZoomSpeed) * fireBallZoomAmount;

            // Apply the calculated scale to the fireball
            obstacle.transform.localScale = fireBallInitialScale + new Vector2(fireBallScale, fireBallScale);
        }

    }
}

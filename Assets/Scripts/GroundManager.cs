using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] AudioSource gameAudio;
    [SerializeField] AudioClip gameOverAudio;
    private bool hasGameOverSoundPlayed = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasGameOverSoundPlayed && other.gameObject.CompareTag("Ball"))
        {
            gameManager.GameOver();
            gameAudio.PlayOneShot(gameOverAudio, 1f);
            hasGameOverSoundPlayed = true;
        }
    }
}

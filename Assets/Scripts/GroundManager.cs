using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] AudioSource gameAudio;
    [SerializeField] AudioClip gameOverAudio;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            gameManager.GameOver();
            gameAudio.PlayOneShot(gameOverAudio, 1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    public GameManager gameManager;  // Reference set dynamically from GameManager
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 2;
    private float ySpawnPos = -6;

    [SerializeField]
    private int pointValue;

    // Reference to the AudioManager
    private AudioManager audioManager;

    private void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

        // Get the AudioManager instance
        audioManager = AudioManager.instance;
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            // Play pop sound effect
            audioManager.PlaySFX(audioManager.popSound);

            gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}

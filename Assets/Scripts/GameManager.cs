using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameTitle;
    public bool isGameActive;
    private bool gameplaystart = true;
    public Button restartButton;
    public Button quitButton;
    public Button playButton;

    private int score;
    
    private void Start()
    {
        if (gameplaystart) {
        gameTitle.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);  
        } else
        {
            Play();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);

            // Instantiate the target prefab
            GameObject targetInstance = Instantiate(targets[index]);

            // Assign GameManager reference to the Target script
            Target targetScript = targetInstance.GetComponent<Target>();
            targetScript.gameManager = this;  // Assign 'this' instance of GameManager
        }
    }

    // Method to update the score and text
    public void UpdateScore(int points)
    {
        if (score == 0 && points <= 0)
        {
            score = 0;
        }
        else
        {
            score += points;
            
        }
        
        scoreText.text = score.ToString();  // Only show the score number
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;   
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Play()
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);  // Initialize score display
        playButton.gameObject.SetActive(false);
        gameTitle.gameObject.SetActive(false);
        gameplaystart = false;
    }

    public void Quit() 
    {
        Application.Quit();
    }
}

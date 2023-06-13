using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject volumeUI;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public bool isGameActive;
    public Button buttonGameOver;
    public Button resumeButton;
    public int livesCounter;
    private AudioSource bgm;
    public Slider volumeLevel;


    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 1f;
        bgm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeLevel.IsActive())
        {
            bgm.volume = volumeLevel.value;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                PauseGame();
            }
            else 
            {
                ResumeGame();
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score : {score}";
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        buttonGameOver.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty, int lives)
    {
        titleScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        volumeUI.gameObject.SetActive(false);
        spawnRate /= difficulty;
        livesCounter = lives;
        isGameActive = true;
        StartCoroutine(nameof(SpawnTarget));
        score = 0;
        scoreText.text = $"Score : {score}";
        livesText.text = $"Lives : {livesCounter}";
    }

    public void DecreaseLife()
    {
        livesCounter --;
        livesText.text = $"Lives : {livesCounter}";
        if (livesCounter == 0)
        {
            GameOver();
        }
    }

    public void PauseGame()
    {
        volumeUI.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        volumeUI.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] GameObject titleScreen;

    int score;
    [SerializeField] int timer = 4;
    float spawnRate = 1;

    public bool isGameOver { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void gameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    IEnumerator SpawnTargets()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(targets[UnityEngine.Random.Range(0, targets.Count)]);
        }
    }

    IEnumerator RunTimer()
    {
        while(timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = "Time: " + timer;
        }
        gameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameOver = false;
        spawnRate /= difficulty;
        score = 0;
        UpdateScore(0);
        timerText.text = "Time: " + timer;
        SetupGUIForGame();
        StartCoroutine(SpawnTargets());
        StartCoroutine(RunTimer());
    }

    private void SetupGUIForGame()
    {
        //hide title screen
        titleScreen.gameObject.SetActive(false);

        //show timer and score
        timerText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
}

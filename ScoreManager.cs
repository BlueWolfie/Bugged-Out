using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;
    public Text scoreText;
    public Text endScreenScoreText;
    public Text highScoreText;

    private int score;
    private float highScore = 0;
    private bool running;
    private float waitSeconds = 1;

    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("ScoreManager instance already made!");
        }

        StopGame();
        HideText();
        HideEndScreenText();
    }

    public void StartGame() {
        running = true;

        score = 0;
        UpdateText();

        ShowText();
        HideEndScreenText();

        StartCoroutine(IncrementScore());
    }

    public void StopGame() {
        running = false;
    }

    
    private IEnumerator IncrementScore() {
        while (running) {
            yield return new WaitForSeconds(waitSeconds);
            score++;
            UpdateText();
        }
    }

    private void UpdateText() {
        scoreText.text = "Score: " + score;
    }

    // Score Counter Methods
    public void ShowText() {
        scoreText.gameObject.SetActive(true);
    }

    public void HideText() {
        scoreText.gameObject.SetActive(false);
    }

    // End Screen Score Methods
    public void ShowEndScreenText() {
        if (score > highScore) {
            highScore = score;
        }

        endScreenScoreText.gameObject.SetActive(true);
        endScreenScoreText.text = "Score: " + score;

        highScoreText.gameObject.SetActive(true);
        highScoreText.text = "High Score: " + highScore;
    }

    public void HideEndScreenText() {
        endScreenScoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
    }

    public float GetScore() {
        return score;
    }
}
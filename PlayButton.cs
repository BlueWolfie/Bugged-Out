using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    public string playSceneName;

    public void PlayGame() {
        SceneManager.LoadScene(playSceneName);
        ScoreManager.instance.StartGame();
        ScoreManager.instance.HideEndScreenText();
    }
}

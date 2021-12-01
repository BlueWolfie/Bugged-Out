using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubstartTransition : MonoBehaviour {
    public string playSceneName;
    private bool transition;
    private float transitionTime = 3;

    void Update() {
        if (Time.time >= transitionTime) {
            SceneManager.LoadScene(playSceneName);
            ScoreManager.instance.StartGame();
            ScoreManager.instance.HideEndScreenText();
        }
    }
}

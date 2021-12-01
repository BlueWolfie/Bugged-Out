using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    public List<GameObject> hearts;
    public PlayerController instance;
    public static HealthManager instance2;

    // Start is called before the first frame update
    private void Start() {
        if (instance2 == null) {
            instance2 = this;
        } else {
            Debug.LogError("HealthManager instance already made!");
        }
    }
    
    public void RemoveHeart() {
        if (hearts.Count > 0) {
            GameObject lastHeart = hearts[hearts.Count - 1];
            lastHeart.SetActive(false);
            hearts.Remove(lastHeart);
        }

        if (hearts.Count == 0) {
            PlayerController.instance.Die();
        }  
    }
}
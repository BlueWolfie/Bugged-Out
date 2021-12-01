using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
  public static PlayerController instance; // This allows this script to be called in other scripts
  public HealthManager instance2;
  
  private bool dead = false;
  public string endSceneName = "End";
  private float timeForScreenTransmition = 2.5f;
  public ParticleSystem deathParticleEffect;
  public float firingSpeedLimit = 0.25f;

  public float horizontalInput;
  public float verticalInput;
  public float speed = 40;
  public float xRange = 70;
  public float zRange = 40;
  public float topZRange = 70;

  // public GameObject projectilePrefab;
  public GameObject[] projectilePrefabs;

  // Start is called before the first frame update
  void Start() {
    if (instance == null) {
      instance = this;
    } else {
      Debug.LogError("PlayerController instance already made!");
    }
  }

  // Update is called once per frame
  void Update() {
    // Left Border
    if (transform.position.x < -xRange) {
      transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
    }

    // Right Border
    if (transform.position.x > xRange) {
      transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
    }

    // Bottom Border
    if (transform.position.z < -zRange) {
      transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
    }

    // Top Border
    if (transform.position.z > topZRange) {
      transform.position = new Vector3(transform.position.x, transform.position.y, topZRange);
    }

    // Firing Speed Limit
    if (Input.GetKeyDown(KeyCode.Space) && !dead) {
      if (Time.time >= firingSpeedLimit) {
        firingSpeedLimit = Time.time + 0.25f;

        // Launch random projectile from player
        int projectileIndex = Random.Range(0, projectilePrefabs.Length);
        Instantiate(projectilePrefabs[projectileIndex], transform.position,
          projectilePrefabs[projectileIndex].transform.rotation);
      }
    }

    if (!dead) {
      horizontalInput = Input.GetAxis("Horizontal");
      transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

      verticalInput = Input.GetAxis("Vertical");
      transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);
    }
  }

  public void Die(){    
    if (!dead) {
      Instantiate(deathParticleEffect, transform.position, transform.rotation);
      ScoreManager.instance.StopGame();
      dead = true;
      StartCoroutine(LoadEndScreen());
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.CompareTag("Enemies")) {
      while (HealthManager.instance2.hearts.Count > 0) {
        HealthManager.instance2.RemoveHeart();
      }
      Die();
    }
  }

  private IEnumerator LoadEndScreen() {
    yield return new WaitForSeconds(timeForScreenTransmition);

    SceneManager.LoadScene(endSceneName);
    ScoreManager.instance.HideText();
    ScoreManager.instance.ShowEndScreenText();
  }
}

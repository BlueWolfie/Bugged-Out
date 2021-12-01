using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject[] bugPrefabs;
    public float spawnRangeX = 15;
    public float spawnPosZ = 20;
    public float startDelay = 12.5f;
    private float spawnInterval = 1.5f;
    private int nextUpdate = 1;
    public float maxSpawnInterval = 2.5f, minSpawnInterval = 0.01f, smoothingFactor = 28.7f;

    // Start is called before the first frame update
    void Start() {
        // StartCoroutine(SpawnCoroutine());
        InvokeRepeating("SpawnRandomBug", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update(){
        if (Time.time >= nextUpdate){
            // Code to debug the time to see if UpdateEverySecond works
            // Debug.Log(Time.time + " >= " + nextUpdate);
            // Change the next update
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            UpdateEverySecond();
        }
     }
     
    // Update is called once per second
    void UpdateEverySecond() {
        if (ScoreManager.instance.GetScore() % 25 == 0 && ScoreManager.instance.GetScore() > 0) {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), -2.2f, spawnPosZ);
            Instantiate(bugPrefabs[2], spawnPos, bugPrefabs[2].transform.rotation);
        }
    }

    private IEnumerator WaitSeconds() {
        yield return new WaitForSeconds(1);
    }

    private IEnumerator SpawnCoroutine() {
        yield return new WaitForSeconds(startDelay);

        while (true) {
            SpawnRandomBug();
            float spawnInterval = GetSpawnInterval();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomBug() {
        int bugIndex = Random.Range(0, 2);
        // Radomly generate animal index and spawn position
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), -2.2f, spawnPosZ);
        if (bugIndex == 1) {
            spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 5, spawnPosZ);
        }
        Instantiate(bugPrefabs[bugIndex], spawnPos, bugPrefabs[bugIndex].transform.rotation);
    }

    private float GetSpawnInterval() {
        float time = ScoreManager.instance.GetScore();
        float spawnInterval = ((maxSpawnInterval - minSpawnInterval) / ((time / smoothingFactor) + 1)) + minSpawnInterval;
        return spawnInterval;
    }
}

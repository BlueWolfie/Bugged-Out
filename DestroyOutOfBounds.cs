using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour {
    // private PlayerController playerController;
    public HealthManager instance2;

    private float topBound = 150;
    private float lowerBound = -50;

    // Update is called once per frame
    void Update() {
        if (transform.position.z > topBound) {
            Destroy(gameObject);
        } else if (transform.position.z < lowerBound) {
            HealthManager.instance2.RemoveHeart();
            if (HealthManager.instance2.hearts.Count > 0) {
                CameraShake.instance.ShakeCamera(1, 0.5f);
            }
            Destroy(gameObject);
        }
    }
}

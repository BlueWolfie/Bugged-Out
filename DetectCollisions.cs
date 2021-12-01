using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Projectile")) {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}

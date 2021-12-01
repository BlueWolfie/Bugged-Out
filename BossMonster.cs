using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour {
    private Animator anim;
    private float health;
    private float timeForDeathAnimation;
    
    public float speed = 7;

    // Start is called before the first frame update
    void Start() {
        health = 100;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (health > 0 && !(health <= 0)) {
            anim.SetBool("Walking", true);
            timeForDeathAnimation = Time.time + 0.75f;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (health <= 0) {
            anim.SetBool("Walking", false);
            anim.SetTrigger("Death Trigger");
            StartCoroutine(WaitSeconds());
            if (Time.time >= timeForDeathAnimation) {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator WaitSeconds() {
        yield return new WaitForSeconds(timeForDeathAnimation);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Projectile")) {
            health = health - 10;
            Destroy(other.gameObject);
        }
    }
}

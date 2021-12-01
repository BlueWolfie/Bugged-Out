using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabMonster : MonoBehaviour {
    private Animator anim;
    private bool dead;
    private float timeForDeathAnimation;
    
    public float speed = 20;

    // Start is called before the first frame update
    void Start() {
        dead = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!dead) {
            anim.SetBool("Walking", true);
            timeForDeathAnimation = Time.time + 0.75f;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        } else if (dead) {
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
            dead = true;
            Destroy(other.gameObject);
        }
    }
}

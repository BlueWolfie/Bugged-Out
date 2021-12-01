using UnityEngine;
using System.Collections;
 
public class CameraShake : MonoBehaviour {
	public static CameraShake instance; // This allows this script to be called in other scripts
	public bool debugMode = false;  	// Test-run/Call ShakeCamera() on start
	public float shakeAmount = 1;       // The amount to shake this frame. 
	public float shakeDuration = 0.5f;  // The duration this frame.
 
	// Readonly values...

	// A percentage (0-1) representing the amount of shake to be applied when setting rotation.
 	float shakePercentage;
    // The initial shake amount (to determine percentage), set when ShakeCamera is called.
 	float startAmount;
    // The initial shake duration, set when ShakeCamera is called.
 	float startDuration;
 
 	bool isRunning = false;	// Is the coroutine running right now?
 
 	public bool smooth;             // Smooth rotation?
 	public float smoothAmount = 5f; // Amount to smooth
 
	void Start () {
		if (debugMode) ShakeCamera ();
		if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Camera Shake Instance Already Created");
        }
	}
 
	void ShakeCamera() {
        // Set default (start) values
		startAmount = shakeAmount;
		startDuration = shakeDuration;
 
        // Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
		if (!isRunning) {
			StartCoroutine (Shake());
		}
	}
 
	public void ShakeCamera (float amount, float duration) {
 
		shakeAmount += amount;          // Add to the current amount.
		startAmount = shakeAmount;      // Reset the start amount, to determine percentage.
		shakeDuration += duration;      // Add to the current time.
		startDuration = shakeDuration;  // Reset the start time.
 
        // Only call the coroutine if it isn't currently running. Otherwise, just set the variables.
		if (!isRunning) {
			StartCoroutine (Shake());
		}
	}
 
 
 	IEnumerator Shake() {
		isRunning = true;
 
		while (shakeDuration > 0.01f) {
            // A Vector3 to add to the Local Rotation
			Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
			rotationAmount.z = 0;   // Don't change the Z; it looks funny.
			rotationAmount.x = 90;  // Keeps the camera view pointing downwards
			
 
            // Used to set the amount of shake (% * startAmount).
			shakePercentage = shakeDuration / startDuration;
 
            // Set the amount of shake (% * startAmount).
			shakeAmount = startAmount * shakePercentage;

            // Lerp the time, so it is less and tapers off towards the end.
			shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);
 
			if (smooth) {
				transform.localRotation = Quaternion.Lerp(transform.localRotation,
                Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            } else {
                // Set the local rotation the be the rotation amount.
				transform.localRotation = Quaternion.Euler (rotationAmount);
            }
			yield return null;
		}

        // Set the local rotation to 0 when done, just to get rid of any fudging stuff.
		transform.localRotation = Quaternion.identity;
		isRunning = false;
	}
 
}
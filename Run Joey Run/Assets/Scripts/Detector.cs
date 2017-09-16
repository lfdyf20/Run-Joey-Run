using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    public float timeSinceLastTrigger = 0f;     //TODO: make it private
    private bool foundClearArea = false;

	// Update is called once per frame
	void Update () {
        timeSinceLastTrigger += Time.deltaTime;
        if (!foundClearArea && timeSinceLastTrigger > 5f && Time.realtimeSinceStartup > 10f) {
            SendMessageUpwards("OnFindClearArea");
            foundClearArea = true;
            Debug.Log("Send message upwards, onFindClearArea, detector");
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag != "Player" ) {
            timeSinceLastTrigger = 0f;
        }      
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag != "Player") {
            timeSinceLastTrigger = 0f;
        }
    }
}

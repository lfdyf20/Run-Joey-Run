using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

    private float expectedArriveTime = 60f;
    private float expectedLandingTime = 10f;
    private bool called = false;
    private bool landing = false;
    private Rigidbody rigidBody;
    private Vector3 landingPosition;
    private Quaternion landingRotation;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}

    void Update() {
        if (called && !landing) {
            if (transform.position.x >= landingPosition.x && transform.position.z >= landingPosition.z) {
                LandHelicopter();
            }
        }

        if (landing && transform.position.y <= landingPosition.y) {
            rigidBody.velocity = Vector3.zero;
            GameObject landingArea = GameObject.FindGameObjectWithTag("LandingArea");
            Destroy(landingArea);
            Debug.Log("Land Successfully");
        }
    }

    public void OndispatchHelicopter() {
        float speedX = (landingPosition.x - transform.position.x) / expectedArriveTime;
        float speedZ = (landingPosition.z - transform.position.z) / expectedArriveTime;
        rigidBody.velocity = new Vector3(speedX, 0, speedZ);
        called = true;
        Debug.Log("Call Helicopter");
    }

    private void LandHelicopter() {
        landing = true;
        transform.position = new Vector3(landingPosition.x, landingPosition.y + 100f, landingPosition.z);
        transform.rotation = landingRotation;
        float speedY = (landingPosition.y - transform.position.y) / expectedLandingTime;
        rigidBody.velocity = new Vector3(0, speedY, 0);
        Debug.Log("Start to landing " + speedY + rigidBody.velocity);
    
    }

    public void SetLanding(Transform playerTransform) {
        landingPosition = playerTransform.position;
        landingRotation = playerTransform.rotation;
    }
}

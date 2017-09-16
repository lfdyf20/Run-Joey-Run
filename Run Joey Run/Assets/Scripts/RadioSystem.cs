using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour {

    public AudioClip initalHeliCall;
    public AudioClip initalCallReply;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
	}
	
    void OnMakeInitialHeliCall(Transform playerTransform) {
        Helicopter helicopter = GameObject.FindObjectOfType<Helicopter>();
        helicopter.SetLanding(playerTransform);
        Debug.Log(name + "OnMakeInitialHeliCall");
        audioSource.clip = initalHeliCall;
        audioSource.Play();

        Invoke("InitialReply", initalHeliCall.length + 1);
    }

    void InitialReply() {
        audioSource.clip = initalCallReply;
        audioSource.Play();
        BroadcastMessage("OndispatchHelicopter");
    }

}

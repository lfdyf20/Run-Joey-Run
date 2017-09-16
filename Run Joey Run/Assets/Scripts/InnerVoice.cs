using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour {

    public AudioClip whatHappened;
    public AudioClip goodLandArea;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 0f;
        audioSource.clip = whatHappened;
        audioSource.Play();
	}
	
	void OnFindClearArea() {
        print(name + " OnFindClearArea");
        audioSource.clip = goodLandArea;
        audioSource.Play();

        Invoke("CallHeli", goodLandArea.length + 1f);
    }

    void CallHeli() {
        SendMessageUpwards("OnMakeInitialHeliCall", transform);
    }

}

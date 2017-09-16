using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Transform playSpawnPoints;
    public GameObject landingAreaPrefab;

    private bool reSpawn = false;
    private Transform[] spawnPoints;
    private bool lastSpawnToggle = false;

	// Use this for initialization
	void Start () {
        spawnPoints = playSpawnPoints.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (lastSpawnToggle != reSpawn) {
            Respawn();
            reSpawn = false;
        } else {
            lastSpawnToggle = reSpawn;
        }
	}

    private void Respawn() {
        int i = Random.Range(1, spawnPoints.Length);
        transform.position = spawnPoints[i].transform.position;
    }

    void OnFindClearArea() {
        Invoke("DropFlare", 3f);
    }

    void DropFlare() {
        Instantiate(landingAreaPrefab, transform.position, transform.rotation);     
    }

}

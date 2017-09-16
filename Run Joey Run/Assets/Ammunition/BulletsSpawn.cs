using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletsSpawn : NetworkBehaviour {

    public GameObject ammunition;
    public float startSpawnAmmunitionTime = 5f;
    public float generateAmmunitionRate = 0.2f;

    private int count = 0;
    private float preSpawnTime = 0;
    private List<string> bulletNames = new List<string>(); 

	// Use this for initialization
	void Start () {
        BulletsManager bulletManager = GameObject.FindObjectOfType<BulletsManager>();
        foreach (GameObject bulletObj in bulletManager.bullets) {
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bulletNames.Add(bullet.bulletName);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isServer) {
            return;
        }
        if (Time.timeSinceLevelLoad > startSpawnAmmunitionTime && count <= 10) {  //TODO: make this a variable
            CmdSpawnAmmunition();
        }
	}

    [Server]
    void CmdSpawnAmmunition() {
        if (Random.value < generateAmmunitionRate * Time.deltaTime) {
            count += 1;
            float posX = Random.Range(0, transform.position.x);
            float posY = transform.position.y;
            float posZ = Random.Range(0, transform.position.z);
            Vector3 spawnPos = new Vector3(posX, posY, posZ);
            var newAmmunition = (GameObject)Instantiate(ammunition, spawnPos, Quaternion.identity);
            string newBulletName = bulletNames[Random.Range(0, bulletNames.Count)];
            Debug.Log(newBulletName);
            newAmmunition.GetComponent<Ammunition>().SetBulletName(newBulletName); 
            NetworkServer.Spawn(newAmmunition);
        }
    }

    [Server]
    public void CmdReduceCount() {
        count -= 1;
    }
}

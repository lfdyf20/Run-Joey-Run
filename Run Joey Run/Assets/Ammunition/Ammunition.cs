using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ammunition : NetworkBehaviour {

    private string bulletName = "BulletCube";
    private Health health;
    private BulletsSpawn bulletsSpawn;

    private void Start() {
        health = GetComponent<Health>();
        bulletsSpawn = GameObject.FindObjectOfType<BulletsSpawn>().GetComponent<BulletsSpawn>();
    }

    private void Update() {
        float posY = transform.position.y;
        if (posY < 30f) {
            ServerDestoryThis();
        }
        if (health.currHealth <= 0) {
            ServerDestoryThis();
        }
    }

    public void SetBulletName(string name) {
        bulletName = name;
    }

    public string GetBulletName() {
        return bulletName;
    }

    private void OnTriggerEnter(Collider other) {
        OnlinePlayer player = other.gameObject.GetComponent<OnlinePlayer>();
        if (player) {
            Debug.Log("Warninh, I get " + bulletName);
            Shooter shooter = player.GetComponentInChildren<Shooter>();
            shooter.SetBullet(bulletName);
            ServerDestoryThis();
        }      
    }

    [Server]
    void ServerDestoryThis() {
        NetworkServer.Destroy(gameObject);
        bulletsSpawn.CmdReduceCount();
    }
}

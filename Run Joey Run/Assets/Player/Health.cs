using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Health :  NetworkBehaviour{

    [SyncVar] public float currHealth = 100f;

    private Slider healthBar;

    void Start() {
        healthBar = GameObject.FindObjectOfType<Slider>();
    }

    void Update() {
        if (isLocalPlayer) {
            healthBar.value = currHealth;
        }
    }

    //[Server]
    public void DealDamage(float damage) {
        if (!isServer) {
            return;
        }
        currHealth -= damage;
    }

    public void ResetHealth() {
        currHealth = 100f;
    }

    [Server]
    public void CmdDestroyObject() {
        NetworkServer.Destroy(gameObject);
    }
}

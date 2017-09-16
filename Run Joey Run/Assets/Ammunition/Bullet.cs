using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{

    public float damage = 10f;
    public float speed = 10f;
    public float life = 5f;
    public float delay = 0.5f;

    [Tooltip("Should be same with the prefab name")]
    public string bulletName;

    private string playerId;

    private void Start() {
        Destroy(gameObject, life);
    }

    private void Update() {
        //transform.Translate(Vector3.forward*speed);
    }

    public void OnInstantiateSetPlayerID(string id) {
        playerId = id;
    }

    private void OnCollisionEnter(Collision collision) {
        Health health = collision.gameObject.GetComponent<Health>();
        OnlinePlayer player = collision.gameObject.GetComponent<OnlinePlayer>();
        if (health) {
            health.DealDamage(damage);
            //Debug.Log(health.currHealth);
            Destroy(gameObject);
        }
        if (player) {
            player.RpcSetLastBeingHitID(playerId);
        }
    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class Bullet : NetworkBehaviour {

//    public float damage = 10f;
//    public float speed = 10f;
//    public float life = 5f;

//    private string playerId;

//    private void Start() {
//        Destroy(gameObject, life);
//    }

//    private void Update() {
//        //transform.Translate(Vector3.forward*speed);
//    }

//    public void OnInstantiateSetPlayerID(string id) {
//        playerId = id;
//    }

//    private void OnCollisionEnter(Collision collision) {
//        Health health = collision.gameObject.GetComponent<Health>();
//        OnlinePlayer otherPlayer = collision.gameObject.GetComponent<OnlinePlayer>();

//        if (health ) {
//            health.DealDamage(damage);
//            //Debug.Log(health.currHealth);
//            Destroy(gameObject);
//        }
//        if (otherPlayer) {
//            otherPlayer.SetLastBeingHitID(playerId, otherPlayer.showID());
//        }
//    }

//        //[Command]
//        //private void RpcMarkPlayerBeingHit(OnlinePlayer player, string myID) {
//        //    player.RpcSetLastBeingHitID(myID);
//        //}

//        //private void DestoryThisBullet() {

//        //    Network.Destroy(gameObject);
//        //}
//}

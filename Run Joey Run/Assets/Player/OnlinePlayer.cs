using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OnlinePlayer : NetworkBehaviour
{

    private Shooter shooter;
    private Health health;
    private Camera eyesCamera;

    private float preFireTime = 0f;
    private string ipID;
    public int score = 0;         //TODO: make it private
    private string lastBeingHitId;
    private NotificationLabel notifictaionLabel;
    private Score scoreBoard;
    private GameObject bulletsParent;
    private BulletsManager bulletsManager;

    [SyncVar]
    private Color bodyColor;
    [SyncVar]
    private Color gunColor;

    MyColorManager myColorManager = new MyColorManager();

    // Use this for initialization
    void Start() {
        shooter = GetComponentInChildren<Shooter>();
        health = GetComponent<Health>();
        eyesCamera = GetComponentInChildren<Camera>();
        ipID = Network.player.ipAddress.ToString();
        notifictaionLabel = Text.FindObjectOfType<NotificationLabel>();
        scoreBoard = Text.FindObjectOfType<Score>();
        bulletsManager = BulletsManager.FindObjectOfType<BulletsManager>();
        bulletsParent = GameObject.Find("BulletsParent");
        if (!bulletsParent) {
            bulletsParent = new GameObject("BulletsParent");
        }
        //bodyColor = myColorManager.ChooseRandomColor();
        //gunColor = myColorManager.ChooseRandomColor();
        CmdChangeColor(ipID);
        //CmdChangeColor(ipID, bodyColor, gunColor);
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        if (Input.GetButtonDown("Fire1")) {
            if (shooter.couldFire()) {
                Vector3 firePos = shooter.transform.position;
                Quaternion fireRotation = shooter.transform.rotation;
                string bulletName = shooter.GetCurrentBulletName();
                CmdFire(firePos, fireRotation, ipID, bulletName);
                shooter.SetLastFireTime(Time.time);
            }
        }
        if (health.currHealth < 0) {
            scoreBoard.ChangeScore(-1);
            CmdHandleDeathAndRespawn(lastBeingHitId, ipID);
        }    
    }

    [Command]
    void CmdFire(Vector3 firePos, Quaternion fireRotation, string id, string bulletName) {
        GameObject bulletPrefab = bulletsManager.GetBulletByName(bulletName);
        //Debug.Log(bulletName);
        var bullet = (GameObject)Instantiate(bulletPrefab,
            firePos, fireRotation);
        bullet.GetComponent<Bullet>().OnInstantiateSetPlayerID(id);
        //Debug.Log("playerid " + id);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bullet.GetComponent<Bullet>().speed;
        NetworkServer.Spawn(bullet);
        bullet.transform.parent = bulletsParent.transform;  //TODO: fix this, not working on clients
        preFireTime = Time.timeSinceLevelLoad;
    }


    [Command]
    void CmdHandleDeathAndRespawn(string killerID, string victimID) {
        if (killerID != ipID) {
            RpcAddScore(killerID, 1);
            RpcAddScore(victimID, -1);
        } else {
            CmdAddScore(1);
            CmdAddScore(-1);
        }
        RpcShowNotification(killerID, victimID);
        CmdShowNotification(killerID, victimID);

        Transform spawn = NetworkManager.singleton.GetStartPosition();
        Vector3 randomPosOffset = new Vector3(Random.Range(-10, 10),
            Random.Range(1, 5), Random.Range(-10, 10));
        var newPlayer = (GameObject)
            Instantiate(NetworkManager.singleton.playerPrefab,
            spawn.position + randomPosOffset,
            spawn.rotation);
        NetworkServer.Destroy(gameObject);
        NetworkServer.ReplacePlayerForConnection(connectionToClient, newPlayer, playerControllerId);
    }

    public string showID() {
        return ipID;
    }

    [ClientRpc]
    public void RpcSetLastBeingHitID(string id) {
        lastBeingHitId = id;
        //Debug.Log("Hit by " + lastBeingHitId);
    }

    [Command]
    private void CmdAddScore(int val) {
        score += val;
        scoreBoard.ChangeScore(val);
        Debug.Log(ipID + " add score " + val);
    }

    [ClientRpc]
    private void RpcAddScore(string killerID, int val) {
        if (killerID == ipID) {
            score += val;
            scoreBoard.ChangeScore(val);
            Debug.Log(ipID + " add score " + val);
        }
    }

    [Command]
    private void CmdShowNotification(string killerID, string victimID) {
        notifictaionLabel.SetNotificationText(killerID, victimID);
    }

    [ClientRpc]
    private void RpcShowNotification(string killerID, string victimID) {
        notifictaionLabel.SetNotificationText(killerID, victimID);
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        GetComponentInChildren<Camera>().enabled = true;
        GetComponentInChildren<AudioListener>().enabled = true;
        GetComponentInChildren<FlareLayer>().enabled = true;
        //bodyColor = myColorManager.ChooseRandomColor();
        //gunColor = myColorManager.ChooseRandomColor();
        //CmdChangeColor(ipID);
        CmdChangeColor(ipID);
    }

    [Command]
    void CmdChangeColor(string id) {
        //void CmdChangeColor(string id, Color bodyColor, Color gunColor) {
        Renderer[] renderArray = GetComponentsInChildren<Renderer>();
        bodyColor = myColorManager.ChooseRandomColor();
        gunColor = myColorManager.ChooseRandomColor();
        renderArray[0].material.color = bodyColor;
        renderArray[1].material.color = gunColor;
        renderArray[2].material.color = gunColor;
        RpcChangeColor(id);
    }

    [ClientRpc]
    void RpcChangeColor(string id) {
        Renderer[] renderArray = GetComponentsInChildren<Renderer>();
        renderArray[0].material.color = bodyColor;
        renderArray[1].material.color = gunColor;
        renderArray[2].material.color = gunColor;
    }

}


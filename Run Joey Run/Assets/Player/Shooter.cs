using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooter : NetworkBehaviour {

    //public GameObject bullet;
    private BulletsManager bulletsManager;
    private Bullet bulletDetail;
    private string bulletName = "BulletBall";
    private float shootDelay = 0.5f;
    private float lastFireTime = 0f;

    void Start() {
        bulletsManager = GameObject.FindObjectOfType<BulletsManager>();
        SetBullet(bulletName);     
    }

    public string GetCurrentBulletName() {
        return bulletName;
    }

    public void SetBullet(string newBulletName) {
        bulletName = newBulletName;
        GameObject newBullet = bulletsManager.GetBulletByName(newBulletName);
        Bullet bulletDetail = newBullet.GetComponent<Bullet>();
        shootDelay = bulletDetail.delay;    
    }

    public bool couldFire() {
        if (Time.time - lastFireTime >= shootDelay) {
            return true;
        }
        return false;
    }

    public void SetLastFireTime(float fireTime) {
        lastFireTime = fireTime;
    }
}

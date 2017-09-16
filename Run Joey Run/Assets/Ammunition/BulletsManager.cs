using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour {

    public GameObject[] bullets;
    private Dictionary<string, GameObject> bulletsNameDict = new Dictionary<string, GameObject>();

    void Start() {
        foreach (GameObject bulletObj in bullets) {
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bulletsNameDict.Add(bullet.bulletName, bulletObj);
        }
        Debug.Log("found " + bulletsNameDict.Keys.Count);
    }

    public GameObject GetBulletByName(string name) {
        if (bulletsNameDict.ContainsKey(name)) {
            return bulletsNameDict[name];
        }
        return bulletsNameDict["BulletBall"];
    }

    public List<string> GetAllBulletsName() {
        List<string> names = new List<string>();
        foreach (string name in bulletsNameDict.Keys) {
            Debug.Log(name);
            names.Add(name);
        }
        Debug.Log(names.Count);
        return names;
    }
}

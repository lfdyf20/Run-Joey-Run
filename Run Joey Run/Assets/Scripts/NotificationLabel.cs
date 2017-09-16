using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationLabel : MonoBehaviour {

    public float delayTime = 3f;

    private Text notificationText;

    void Start() {
        notificationText = GetComponent<Text>();
        ClearNotification();
    }

    public void SetNotificationText(string killerID, string victimID) {
        string notification = killerID + " killed " + victimID;
        notificationText.text = notification;
        Invoke("ClearNotification", delayTime);
    }

    private void ClearNotification() {
        notificationText.text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Score : NetworkBehaviour {


    private int currScore = 0;
    private Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        scoreText.text = currScore.ToString();
	}

    public void ChangeScore(int score) {
        currScore += score;
        if (currScore < 0) {
            currScore = 0;
        }
        scoreText.text = currScore.ToString();
    }

    public int showCurrScore() {
        return currScore;
    }
}

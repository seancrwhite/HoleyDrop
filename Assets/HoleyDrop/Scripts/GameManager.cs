using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    private int score, highScore;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        score = 0;
        highScore = PlayerPrefs.GetInt("High Score");
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Score: " + score++;

        if (score > highScore)
        {
            text.color = Color.yellow;
            PlayerPrefs.SetInt("High Score", highScore);
        }
	}
}

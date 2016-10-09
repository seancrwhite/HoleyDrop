using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetHighScore : MonoBehaviour {
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        text.text = PlayerPrefs.GetInt("High Score").ToString();
    }
}

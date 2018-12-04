using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    public Text message;
    public Text score;
    public Button button;

    float delay = 2;

	void Start () {

        if (PlayerController.main && PlayerController.main.currentFuel <= 0) message.text = "No Fuel";

        string text = "Score : " + (int)PlayerController.score;
        string[] chars = text.Split();
        score.text = string.Join(" ", chars);
	}
	
	void Update () {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
            if (delay <= 0) {
                button.gameObject.SetActive(true);
                score.gameObject.SetActive(true);
            }
        }
	}
    public void ButtonOkay()
    {
        SceneManager.LoadScene("Title");
    }
}

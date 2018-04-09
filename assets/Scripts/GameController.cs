using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text endText;
	public Text scoreText;
	private int remaining;

	// Use this for initialization
	void Start () {
		endText.text = "";
		remaining = 3;
		scoreText.text = "Obtacles Remaining: " + remaining.ToString();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			Time.timeScale = 1;
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void GameOverLose()
	{
		endText.text = "You Lose";
		Time.timeScale = 0;
	}

	public void GameOverWin()
	{
		endText.text = "You Win!";
		Time.timeScale = 0;
	}

	public void addScore()
	{
		remaining -= 1;
		scoreText.text = "Obtacles Remaining: " + remaining.ToString();
		if(remaining <= 0)
		{
			GameOverWin();
		}
	}
}

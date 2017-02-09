using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public bool ableToStart, gameStarted, lost;
	public int score = 0;

	public delegate void GameManagerEvents ();

	public event GameManagerEvents ResetGameEvent;
	public event GameManagerEvents StartGameEvent;
	public event GameManagerEvents GameOverEvent;

	public void FirstTap ()
	{
		gameStarted = true;

		if (StartGameEvent != null)
			StartGameEvent ();
	}

	public void IncreaseScore ()
	{
		score++;
		UIScore.Instance.IncreaseScore (score);
	}

	public void GameOver ()
	{
		gameStarted = false;
		ableToStart = false;
		lost = true;
		UIGameOver.Instance.ShowGameOver ();

		if (GameOverEvent != null)
			GameOverEvent ();
	}

	public void Reset ()
	{
		lost = false;
		score = 0;
		ableToStart = false;
		gameStarted = false;

		if (ResetGameEvent != null)
			ResetGameEvent ();
	}
}

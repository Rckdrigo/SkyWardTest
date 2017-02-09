using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

	public bool lost;
	public int score = -1;

	public void IncreaseScore ()
	{
		score++;
		UIScore.Instance.IncreaseScore (score);
	}


	public void GameOver ()
	{
		lost = true;
		UIGameOver.Instance.ShowGameOver (true);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : Singleton<UIGameOver>
{

	public GameObject gameOverLabel;

	public void ShowGameOver ()
	{
		gameOverLabel.SetActive (true);
	}

	public void Reset ()
	{
		gameOverLabel.SetActive (false);
		GameManager.Instance.Reset ();
	}
}

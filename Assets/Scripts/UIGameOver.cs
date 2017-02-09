using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : Singleton<UIGameOver>
{

	public GameObject gameOverLabel;

	public void ShowGameOver (bool lost)
	{
		gameOverLabel.SetActive (lost);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : Singleton<UIScore>
{

	public Text scoreLabel;

	public void IncreaseScore (int score)
	{
		scoreLabel.text = score.ToString ();
	}

	public void Reset ()
	{
		scoreLabel.text = "0";
	}
}

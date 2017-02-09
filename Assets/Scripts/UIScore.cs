using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : Singleton<UIScore>
{

	public Text scoreLabel;

	void Start ()
	{
		GameManager.Instance.ResetGameEvent += () => {
			scoreLabel.text = "0";
		};
	}

	public void IncreaseScore (int score)
	{
		scoreLabel.text = score.ToString ();
	}

}

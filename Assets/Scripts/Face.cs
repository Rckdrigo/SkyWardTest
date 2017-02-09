using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public struct FaceCollider
//{
//	public Vector3 position;
//	public Vector3 size;
//}

public enum FaceDirection
{
	Top,
	Right,
	Left
}

public enum PowerUpType
{
	normal,
	speed,
	spin
}


public class Face : MonoBehaviour
{
	public FaceDirection direction;
	public int index;

	public PowerUpType powerUp;
	public SpriteRenderer icon;

	public Vector3 faceDir;
	public Vector3 initialScale;


	public void SetupRandomPowerUp ()
	{
		powerUp = Random.value > 0.5f ? PowerUpType.speed : PowerUpType.spin;

		GetComponent<MeshRenderer> ().material.color = Color.yellow;
		icon.enabled = true;

		if (powerUp == PowerUpType.speed)
			icon.sprite = ScenarioController.Instance.speedTexture;
		else if (powerUp == PowerUpType.spin)
			icon.sprite = ScenarioController.Instance.spinTexture;
	}


	public IEnumerator GrowAnimation ()
	{
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.01f);
			transform.localScale = Vector3.Lerp (transform.localScale, ScenarioController.Instance.GetFaceInitialScale (direction), (float)i / 20);
		}
	}

	public IEnumerator ShrinkAnimation ()
	{
		for (int i = 0; i < 20; i++) {
			yield return new WaitForSeconds (0.01f);
			transform.localScale = Vector3.Lerp (transform.localScale, Vector3.zero, (float)i / 20);
		}
		gameObject.SetActive (false);
	}
}

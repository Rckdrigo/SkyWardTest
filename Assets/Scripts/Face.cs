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


	//	Animator anim;

	void Start ()
	{
//		anim = GetComponent<Animator> ();
//
//		switch (direction) {
//		case FaceDirection.Left:
//			faceDir = Vector3.right;
//			break;
//
//		case FaceDirection.Right:
//			faceDir = Vector3.forward;
//			break;
//
//		case FaceDirection.Top:
//			faceDir = Vector3.up;
//			break;
//		}
//
//		faceDir *= 0.25f;
	}

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


}

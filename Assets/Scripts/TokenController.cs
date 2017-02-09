using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TokenController : Singleton<TokenController>
{
	public Transform pivot, activeToken;
	[Range (100, 400)] public float angularSpeed = 200;
	public float collisionRadius = 0.5f;

	Face currentFace = null;
	float initialDistance = 0;

	public float extraSpeedPowerUp = 150.0f;
	float speedBonus = 0;
	int sense = 1;

	void Start ()
	{
		initialDistance = Vector3.Distance (pivot.position, activeToken.position);
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {

			var facesOnCollision = 
				from face in ScenarioController.Instance.totalFaces
				where (Vector3.Distance (activeToken.position, face.transform.position)) < collisionRadius
				orderby face.index descending
				select face;

			foreach (var face in facesOnCollision.ToList()) {
				currentFace = face;
				break;
			}

			if (currentFace != null) {
//				Debug.Log ("The next face " + currentFace.index);
				ScenarioController.Instance.ChangeToFace (currentFace.index);

				if (currentFace.powerUp == PowerUpType.spin)
					ChangeRotation ();
				else if (currentFace.powerUp == PowerUpType.speed)
					ChangeSpeed (true);
				else
					ChangeSpeed (false);

				Transform temp = pivot;
				pivot = activeToken;
				activeToken = temp;

				pivot.transform.position = currentFace.transform.position + currentFace.faceDir * 0.65f;
				activeToken.transform.position = new Vector3 (activeToken.transform.position.x, pivot.transform.position.y, activeToken.transform.position.z);

				activeToken.transform.position = pivot.transform.position + (activeToken.transform.position - pivot.transform.position).normalized * initialDistance;

				currentFace = null;
			} else {
				print ("LOSE");
			}
		}

		activeToken.RotateAround (pivot.position, Vector3.up, (angularSpeed + speedBonus) * sense * Time.deltaTime);
	
	}

	public void SetTokenDirection (FaceDirection direction)
	{
		Vector3 newTokenDirection;

		switch (direction) {
		case FaceDirection.Left:
			newTokenDirection = Vector3.right;
			break;

		case FaceDirection.Right:
			newTokenDirection = Vector3.forward;
			break;

		default:
			newTokenDirection = Vector3.up;
			break;
		}

		pivot.transform.up = newTokenDirection;
		activeToken.transform.up = newTokenDirection;
	}

	void ChangeRotation ()
	{
		sense *= -1;
	}

	void ChangeSpeed (bool fast)
	{
		speedBonus = fast ? extraSpeedPowerUp : 0f;
	}

}

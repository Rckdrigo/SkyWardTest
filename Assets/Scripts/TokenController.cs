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

	public int maxRotationAmount = 3;
	int rotationAmount = 0;
	public float penaltySpeedUp = 100.0f;

	int sense = 1;

	float currentAngle = 0f;

	Vector3 initialPivotPosition, initialActiveTokenPosition;

	void Start ()
	{
		initialDistance = Vector3.Distance (pivot.position, activeToken.position);
		initialPivotPosition = pivot.position;
		initialActiveTokenPosition = activeToken.position;

		GameManager.Instance.ResetGameEvent += () => {
			speedBonus = 0;
			rotationAmount = 0;
			sense = 1;
			currentAngle = 0f;

			currentFace = null;

			pivot.position = initialPivotPosition;
			activeToken.position = initialActiveTokenPosition;

			pivot.up = activeToken.up = Vector3.up;
		};
	}

	void Update ()
	{
		if (!GameManager.Instance.lost) {
			if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && GameManager.Instance.ableToStart) {

				if (!GameManager.Instance.gameStarted)
					GameManager.Instance.FirstTap ();

				var facesOnCollision = 
					from face in ScenarioController.Instance.totalFaces
					where (Vector3.Distance (activeToken.position, face.transform.position)) < collisionRadius
					orderby Vector3.Distance(face.transform.position, activeToken.position) descending
					select face;

				foreach (var face in facesOnCollision.ToList()) {
					currentFace = face;
					break;
				}

				if (currentFace != null) {
					currentAngle = 0;
					rotationAmount = 0;



					if (currentFace.powerUp == PowerUpType.spin)
						ChangeRotation ();
					else if (currentFace.powerUp == PowerUpType.speed)
						ChangeSpeed (true);
					else
						ChangeSpeed (false);

					Transform temp = pivot;
					pivot = activeToken;
					activeToken = temp;

					ScenarioController.Instance.ChangeToFace (currentFace.index);
					pivot.transform.position = currentFace.transform.position + currentFace.faceDir * 0.65f;
					activeToken.transform.position = new Vector3 (activeToken.transform.position.x, pivot.transform.position.y, activeToken.transform.position.z);

					activeToken.transform.position = pivot.transform.position + (activeToken.transform.position - pivot.transform.position).normalized * initialDistance;

					currentFace = null;
				} else {
					GameManager.Instance.GameOver ();
					print ("LOSE");
				}
			}
			float angle = (angularSpeed + speedBonus) * sense * Time.deltaTime;
			activeToken.RotateAround (pivot.position, Vector3.up, angle);
			currentAngle += angle;

			if (currentAngle > 360 && GameManager.Instance.gameStarted) {
				rotationAmount++;
				if (rotationAmount == maxRotationAmount)
					GameManager.Instance.GameOver ();
				else {
					speedBonus += penaltySpeedUp;
					currentAngle = 0;
				}
			}
		}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TokenController : Singleton<TokenController>
{

	public Transform pivot, activeToken;
	[Range (100, 400)] public float angularSpeed = 200;

	// Update is called once per frame
	void Update ()
	{
		activeToken.RotateAround (pivot.position, pivot.up, angularSpeed * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Transform temp = pivot;
			pivot = activeToken;
			activeToken = temp;
		}
			
	}
}

using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	public float smoothness = 3f;
	private Vector3 initialVector;

	void Start ()
	{
		initialVector = Camera.main.transform.position - TokenController.Instance.pivot.transform.position;
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, TokenController.Instance.pivot.transform.position + initialVector, Time.deltaTime * smoothness);
	}
}

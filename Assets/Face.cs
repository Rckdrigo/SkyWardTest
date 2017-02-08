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


[RequireComponent (typeof(BoxCollider))]
public class Face : MonoBehaviour
{
	public FaceDirection direction;
	Collider collider;

	void Start ()
	{
		collider = GetComponent<Collider> ();
		collider.isTrigger = true;
	}

	void OnTriggerEnter (Collider col)
	{
		GetComponent<MeshRenderer> ().material.color = Color.green;
	}


	void OnTriggerExit (Collider col)
	{
		GetComponent<MeshRenderer> ().material.color = Color.white;
	}

	//	public new FaceCollider collider;
	//
	//	public void SetFaceCollider ()
	//	{
	//		switch (direction) {
	//		case FaceDirection.Top:
	//			collider.position = transform.position + Vector3.up * 0.5f;
	//			break;
	//		case FaceDirection.Right:
	//			collider.position = transform.position + Vector3.right * 0.5f;
	//			break;
	//		case FaceDirection.Left:
	//			collider.position = transform.position + Vector3.left * 0.5f;
	//			break;
	//		}
	//
	//		collider.size = Vector3.one;
	//
	//		print ("Face collider " + name + "\tCenter" + collider.position + "\tSize" + collider.size);
	//	}

}

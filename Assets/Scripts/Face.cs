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
	public int index;

	public Vector3 faceDir;
	//	Animator anim;

	void Start ()
	{
//		anim = GetComponent<Animator> ();

		switch (direction) {
		case FaceDirection.Left:
			faceDir = Vector3.right;
			break;

		case FaceDirection.Right:
			faceDir = Vector3.forward;
			break;

		case FaceDirection.Top:
			faceDir = Vector3.up;
			break;
		}

		faceDir *= 0.25f;
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

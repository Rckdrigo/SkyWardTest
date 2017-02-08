using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode ()]
[RequireComponent (typeof(Cell))]
public class CellCreationHelper : MonoBehaviour
{

	public GameObject leftFace, rightFace, topFace;
	public GameObject red, blue, green;

	public void SetupFace (bool hasLeftFace, bool hasRightFace, bool hasTopFace, int selection)
	{

//		if (hasLeftFace)
//			leftFace.GetComponent<Face> ().SetFaceCollider ();
//		if (hasRightFace)
//			rightFace.GetComponent<Face> ().SetFaceCollider ();
//		if (hasTopFace)
//			topFace.GetComponent<Face> ().SetFaceCollider ();

		leftFace.SetActive (hasLeftFace);
		rightFace.SetActive (hasRightFace);
		topFace.SetActive (hasTopFace);

		switch (selection) {
		case 0:
			GetComponent<Cell> ().nextCellDirection = green;
			green.SetActive (true);
			break;
		
		case 1:
			GetComponent<Cell> ().nextCellDirection = red;
			red.SetActive (true);
			break;

		case 2:
			GetComponent<Cell> ().nextCellDirection = blue;
			blue.SetActive (true);
			break;
		}

		DestroyImmediate (this);
	}




}

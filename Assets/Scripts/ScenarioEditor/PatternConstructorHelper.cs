using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Pattern))]
public class PatternConstructorHelper : MonoBehaviour
{

	public List<GameObject> cells = new List<GameObject> ();

	public void SetNewCell (GameObject cell, int direction, bool moveUp)
	{
		cell.transform.parent = transform;
		if (cells.Count > 0) {
			switch (direction) {
			case 0:
				cell.transform.position = cells [cells.Count - 1].transform.position + Vector3.back;
				break;

			case 1:
				cell.transform.position = cells [cells.Count - 1].transform.position + Vector3.left;
				break;

			case 2:
				cell.transform.position = cells [cells.Count - 1].transform.position + Vector3.forward;
				break;
			}

			if (moveUp)
				cell.transform.position += Vector3.up;
		}

		cells.Add (cell);
	}

	public void DeleteLastCell ()
	{
		GameObject o = cells [cells.Count - 1];
		cells.RemoveAt (cells.Count - 1);

		DestroyImmediate (o);
	}

	public void DeleteAllCells ()
	{
		while (transform.childCount > 0) {
			DeleteLastCell ();
		}
		cells.Clear ();
	}

	public void Save ()
	{
		GetComponent<Pattern> ().cells = cells;
		GetComponent<Pattern> ().endOfPatternReference = cells [cells.Count - 1].GetComponent<Cell> ().nextCellDirection;
	}

	void OnDrawGizmos ()
	{
		if (cells.Count > 0) {
			Gizmos.color = Color.red;
			Gizmos.DrawRay (cells [cells.Count - 1].transform.position, Vector3.left * 3);

			Gizmos.color = Color.blue;
			Gizmos.DrawRay (cells [cells.Count - 1].transform.position, Vector3.forward * 3);

			Gizmos.color = Color.green;
			Gizmos.DrawRay (cells [cells.Count - 1].transform.position, Vector3.back * 3);
		}
	}
}

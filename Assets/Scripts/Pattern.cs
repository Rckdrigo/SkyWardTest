using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
	public GameObject endOfPatternReference;
	public List<GameObject> cells;

	public List<Face> GetFaces ()
	{
		List<Face> f = new List<Face> ();
		for (int i = 0; i < cells.Count; i++) {
			Cell c = cells [i].GetComponent<Cell> ();
			if (c.top.isActiveAndEnabled)
				f.Add (c.top);
			if (c.right.isActiveAndEnabled)
				f.Add (c.right);
			if (c.left.isActiveAndEnabled)
				f.Add (c.left);
		}

		return f;
	}
}

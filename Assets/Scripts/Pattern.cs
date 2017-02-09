using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pattern : MonoBehaviour
{
	public GameObject endOfPatternReference;
	public List<GameObject> cells;

	[Range (0f, 100f)]public float powerUpChance;
	public int maxPowerUpFaces = 0;

	public List<Face> GetFaces (int index)
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

		if (index > 0)
			SetupPowerUps (f);

		return f;
	}

	void SetupPowerUps (List<Face> faces)
	{
		Random.InitState ((int)Time.time + (int)transform.position.x);

		var randomizedList = 
			from i in faces
			orderby Random.value
			select i;

		int currentPowerUps = 0;

		foreach (Face f in randomizedList) {
			float r = Random.value;
			if (r < powerUpChance) {
				f.SetupRandomPowerUp ();
				currentPowerUps++;
				if (currentPowerUps == maxPowerUpFaces)
					break;
			}
		}
	}
}

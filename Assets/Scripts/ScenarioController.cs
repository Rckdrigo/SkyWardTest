using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScenarioController : Singleton<ScenarioController>
{
	[HideInInspector] public List<Pattern> patterns;

	public int index = 0;
	public int nextFacesWindow = 10;
	public List<Face> totalFaces = new List<Face> ();

	public Sprite speedTexture, spinTexture;

	List<Face> GetNextFaces (int count)
	{
		var nextFaces =
			from face in totalFaces
			where (face.index < index + count + 1)
			orderby face.index ascending
			select face;

		return nextFaces.ToList <Face> ();
	}

	public void GetAllFaces (List<Pattern> patterns)
	{
		this.patterns = patterns;
		for (int i = 0; i < patterns.Count; i++)
			totalFaces.AddRange (patterns [i].GetFaces (i));

		for (int i = 0; i < totalFaces.Count; i++)
			totalFaces [i].index = i;

		ChangeToFace (0);
	}

	public void ChangeToFace (int newIndex)
	{


		index = newIndex;
		TokenController.Instance.activeToken.up = totalFaces [index].faceDir;
//		List<Face> temp = GetNextFaces (nextFacesWindow);
//
//		for (int i = 0; i < temp.Count; i++)
//			temp [i].GetComponent<MeshRenderer> ().material.color = Color.yellow;



		var previousFaces = 
			from face in ScenarioController.Instance.totalFaces
			where face.index < index - 3
			select face;

		foreach (var face in previousFaces.ToList()) {
//			totalFaces.Remove (face);
//			face.GetComponent<MeshRenderer> ().material.color = Color.red;
		}
	}
		
}

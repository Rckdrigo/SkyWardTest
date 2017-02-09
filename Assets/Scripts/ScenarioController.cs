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

	void Start ()
	{
		GameManager.Instance.GameOverEvent += DestroyScenario;
	}

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

		for (int i = 0; i < totalFaces.Count; i++) {
			totalFaces [i].index = i;
		}

		ChangeToFace (0);
	}

	public void ChangeToFace (int newIndex)
	{
		GameManager.Instance.IncreaseScore ();

		index = newIndex;
		TokenController.Instance.activeToken.up = totalFaces [index].faceDir;

		List<Face> temp = GetNextFaces (nextFacesWindow);

		for (int i = 0; i < temp.Count; i++) {
			StartCoroutine (temp [i].GrowAnimation ());
		}

		if (GameManager.Instance.gameStarted) {
			var previousFaces = 
				from face in ScenarioController.Instance.totalFaces
				where face.transform.position.x - 0.75f > TokenController.Instance.pivot.position.x
				select face;

			foreach (var face in previousFaces.ToList()) {
				StartCoroutine (face.ShrinkAnimation ());
			}
		}
	}

	public void DestroyScenario ()
	{
		foreach (Pattern p in patterns)
			Destroy (p.gameObject);

		patterns.Clear ();
		totalFaces.Clear ();
		index = 0;
	}
}

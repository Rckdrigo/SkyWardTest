using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCreator : MonoBehaviour
{

	[Range (1, 100)][SerializeField] private int patternCount = 10;
	public List<Pattern> patterns;
	private GameObject[] patternPrefabs;

	public int currentFace;

	// Use this for initialization
	void Start ()
	{
		patternPrefabs = Resources.LoadAll <GameObject> ("Patterns");

		for (int i = 0; i < patternCount; i++) {
			int random = Random.Range (0, patternPrefabs.Length);
			GameObject pattern = Instantiate (patternPrefabs [random]) as GameObject;
			if (i > 0)
				pattern.transform.position = patterns [i - 1].endOfPatternReference.transform.position;

			patterns.Add (pattern.GetComponent<Pattern> ());
		}

		ScenarioController.Instance.GetAllFaces (patterns);

		Destroy (this);
	}

}

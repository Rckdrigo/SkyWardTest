using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioCreator : MonoBehaviour
{

	[Range (1, 100)][SerializeField] private int patternCount = 10;
	public List<Pattern> patterns;
	[Range (1, 15)][SerializeField] private int initialFaces = 15;

	private GameObject[] patternPrefabs;


	void Start ()
	{
		CreateScenario ();
		GameManager.Instance.ResetGameEvent += () => {
			patterns.Clear ();
			CreateScenario ();
		};
	}

	void CreateScenario ()
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
		StartCoroutine (ShowFirstFaces ());
	}

	IEnumerator ShowFirstFaces ()
	{
		for (int i = 0; i < initialFaces; i++) {
			StartCoroutine (ScenarioController.Instance.totalFaces [i].GrowAnimation ());
			yield return new WaitForSeconds (0.1f);
		}

		GameManager.Instance.ableToStart = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : Singleton<ScenarioController>
{

	public List<Pattern> patterns;
	public int currentPattern = 0;

	//	void OnDrawGizmos ()
	//	{
	//		Gizmos.color = Color.blue;
	//		foreach (Pattern p in patterns) {
	//			Vector3 centre = (p.transform.position + p.endOfPatternReference.transform.position) / 2;
	//			float size = Vector3.Distance (p.transform.position, p.endOfPatternReference.transform.position);
	//
	//			Gizmos.DrawWireCube (centre, Vector3.one * size);
	//		}
	//	}
}

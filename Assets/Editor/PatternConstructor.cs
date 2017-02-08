using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class PatternConstructor : EditorWindow
{

	GameObject pattern;
	GameObject cell;
	bool creatingCell;
	bool moveUp;

	string[] nextCellDirection = { "Green", "Red", "Blue" };
	int selection = 1;

	bool hasTopFace, hasRightFace, hasLeftFace;
	string patternName = "Pattern";
	PatternConstructorHelper helper;


	// Add menu named "My Window" to the Window menu
	[MenuItem ("MyPlugin/Create Pattern")]
	static void Init ()
	{
			
		// Get existing open window or if none, make a new one:
		PatternConstructor window = (PatternConstructor)EditorWindow.GetWindow (typeof(PatternConstructor));
		window.Show ();


	}

	void OnGUI ()
	{
		GUILayout.Label ("Setting up the pattern", EditorStyles.boldLabel);
		GUILayout.BeginHorizontal ();

		if (pattern == null) {
			if (GUILayout.Button ("Find/Create pattern reference")) {
				pattern = GameObject.Find ("PatternConstructor");
				if (pattern == null) {
					pattern = new GameObject ();
					pattern.name = "PatternConstructor";
					pattern.AddComponent<PatternConstructorHelper> ();
				}
				selection = 0;
			}
		}
		pattern = EditorGUILayout.ObjectField ("", pattern, typeof(GameObject), true) as GameObject;
		if (pattern != null)
			helper = pattern.GetComponent<PatternConstructorHelper> ();

		GUILayout.EndHorizontal ();
		EditorGUILayout.Space ();

		cell = EditorGUILayout.ObjectField ("Cell prefab", Resources.Load <GameObject> ("Cell"), typeof(GameObject), true) as GameObject;

		EditorGUILayout.Space ();

		if (pattern != null) {
			GUILayout.BeginHorizontal ();
			hasTopFace = GUILayout.Toggle (hasTopFace, "Has top face?");
			hasLeftFace = GUILayout.Toggle (hasLeftFace, "Has left face?");
			hasRightFace = GUILayout.Toggle (hasRightFace, "Has right face?");
			GUILayout.EndHorizontal ();

			EditorGUILayout.Space ();

			if (helper.cells.Count > 0) {
				GUILayout.Label ("Direction of the next cell");
				selection = GUILayout.SelectionGrid (selection, nextCellDirection, 3);

				EditorGUILayout.Space ();
				moveUp = GUILayout.Toggle (moveUp, "Increase Y value?");

				EditorGUILayout.Space ();


				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Create cell"))
					CreateCell (selection);

				if (GUILayout.Button ("Remove last cell"))
					helper.DeleteLastCell ();
			
				GUILayout.EndHorizontal ();

				EditorGUILayout.Space ();

				GUILayout.BeginHorizontal ();
				GUILayout.Label ("Name: ");
				patternName = GUILayout.TextField (patternName, 25);
				GUILayout.EndHorizontal ();


				EditorGUILayout.Space ();

				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("Save pattern"))
					SavePattern ();
			
				if (GUILayout.Button ("Reset pattern"))
					ResetPattern ();

				GUILayout.EndHorizontal ();
			} else {
				if (GUILayout.Button ("Create cell")) {
					CreateCell (1);
				}
			}
		}
	}

	void CreateCell (int selection)
	{
		GameObject o = Instantiate (cell) as GameObject;
		o.GetComponent<CellCreationHelper> ().SetupFace (hasLeftFace, hasRightFace, hasTopFace, selection);

		helper.SetNewCell (o, selection, moveUp);
	}

	void SavePattern ()
	{
		if (!AssetDatabase.IsValidFolder ("Assets/Resources/Patterns"))
			AssetDatabase.CreateFolder ("Assets/Resources", "Patterns");
		string prefabPath = "Assets/Resources/Patterns/" + patternName + ".prefab";
		AssetDatabase.DeleteAsset (prefabPath);

		helper.Save ();

		DestroyImmediate (helper);
		PrefabUtility.CreatePrefab (prefabPath, pattern);
		pattern.name = patternName;
		pattern = null;
		selection = 1;

	}

	void ResetPattern ()
	{
		helper.DeleteAllCells ();
		selection = 1;
	}
}
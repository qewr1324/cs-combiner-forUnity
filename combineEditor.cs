using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(combiner))]
public class combineEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		combiner _test = (combiner)target;
		EditorGUILayout.Space (); // sapce

		// label
		GUILayout.Box ("Mesh Combiner", GUILayout.ExpandWidth (true));
		EditorGUILayout.Space (); // sapce

		// massage for help
		EditorGUILayout.HelpBox ("\n     New <Combine>: Create new mesh from you child mesh.\n\n     Clear <Inspector>: If you're done, use this.!!\n", MessageType.None);
		EditorGUILayout.Space (); // sapce

		GUILayout.BeginHorizontal (); // fix begin scale
		// for new combine mesh
		if (GUILayout.Button ("New\n*Combine*")) {
			_test.combine ();
		}
		EditorGUILayout.Space (); // sapce
		// for delete combiner script
		if (GUILayout.Button ("Clear\n*Inspector*")) {
			_test.clear ();
		}
		GUILayout.EndHorizontal (); // fix end scale
		EditorGUILayout.Space (); // sapce
	}
}
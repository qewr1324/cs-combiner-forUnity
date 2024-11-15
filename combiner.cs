using UnityEngine;
using UnityEditor;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))] // import requirecomponent when this script is active.
public class combiner : MonoBehaviour
{
	// destroy script when start game.
	private void Start ()
	{
		Destroy (this);
	}
	// begin.
	[MenuItem ("cs-combiner/Combine in hierarchy")]
	public static void parent ()
	{
		var mesh = Selection.gameObjects; // get selected object.
		if (mesh.Length == 0) {
			// error masages.
			EditorUtility.DisplayDialog (" Note", "\nPlease select the meshes in\n* hierarchy *\n", "ok");
		} else if (mesh.Length == 1) {
			EditorUtility.DisplayDialog (" Note", "\nYou only selected one mesh, please select more to combine them together\n", "ok");
		} else {
			GameObject obj = new GameObject (); // create new gameobject for the parent.
			obj.name = mesh [0].name + "<>"; // set a name for the gameobject.
			obj.gameObject.transform.parent = mesh [0].gameObject.transform.parent; // set gameobject transform from selected objects.
			for (int b = 0; b < mesh.Length; b++) { // loop for set selected object into the new gameobject as child.
				mesh [b].gameObject.transform.parent = obj.gameObject.transform;
			}
			hierarcy (obj); // call hierarchy function.
		}
	}
	// combine mesh from hirearchy.
	public static void hierarcy (GameObject obj)
	{
		try {
			obj.AddComponent<combiner> (); // add this script to selected gameobject.
			obj.GetComponent<combiner> ().combine (); // get script component and call combine function.
		} catch {
			// show masage to the client, if failed.
			EditorUtility.DisplayDialog (" Note", "\nTry again, there is a problem.\nMaybe you are choose an object into asset folder.\nYou most choose an object into * Hierarchy *.\n", "ok");
		}
	}
	// combine function.
	internal void combine ()
	{
		CombineInstance[] file = new CombineInstance[transform.childCount]; // get all childs from gameobject.
		int index = 0; // create an count.
		foreach (Transform child in gameObject.transform) { // create loop for get all gameobjects in hirearcy.
			MeshFilter filter = child.GetComponent<MeshFilter> (); // get all meshfilters.
			file [index].mesh = filter.sharedMesh; // get all meshes info.
			file [index].transform = child.localToWorldMatrix; // turn local space to world space in pivot.
			child.gameObject.SetActive (false); // turn off child object in the hierarchy.
			index++; // count++.
		}
		Mesh mesh = new Mesh (); // create new mesh.
		mesh.CombineMeshes (file); // combine all mesh into one mesh.
		transform.GetComponent<MeshFilter> ().sharedMesh = mesh; // set info about combined mesh.
		transform.gameObject.SetActive (true); // turn on the combined mesh.
		MeshUtility.Optimize (mesh);
	}
	// masage for scripts author.
	[MenuItem ("cs-combiner/Author")]
	public static void author ()
	{
		EditorUtility.DisplayDialog (" Author", "\nauthor : Amirhussein Mohammadi Fard\n\nwww.github.com/qewr1324\nwww.artstation.com/gurbesabzi\n\nthanks to:\nYern Games\nGameDevBox\nSunny Valley Studio\n", "ok");
	}
	// masage for licence.
	[MenuItem ("cs-combiner/Licence")]
	public static void licence ()
	{
		EditorUtility.DisplayDialog (" Licence", "\nApache License\nVersion 2.0, January 2004\nhttp://www.apache.org/licenses/\n\n\nVersion 5.2.4", "ok");
	}
	// clear function.
	internal void clear ()
	{
		try{
			DestroyImmediate (this);
		}catch{
			
		}
	}
}
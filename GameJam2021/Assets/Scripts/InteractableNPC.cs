using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class InteractableNPC : MonoBehaviour {

	GameObject dialogBand;
	List<string> dialogs = new List<string>();
	List<string>.Enumerator enumerator;
	public TextAsset dialogFile;

	public string npcName;

	// Use this for initialization
	void Start () {
		parseTextAsset (dialogFile);

		dialogBand = GameObject.FindWithTag ("DialogBand");
		//dialogBand.SetActive (false);

		enumerator = dialogs.GetEnumerator ();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Entered NPC CollideBox");
			Player player = other.gameObject.GetComponent<Player>();
			player.SetInteractableObject (this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Exited NPC CollideBox");
			Player player = other.gameObject.GetComponent<Player>();
			player.ClearInteractableObject ();
			dialogBand.SetActive (false);
			enumerator = dialogs.GetEnumerator ();
		}
	}

	void Interact(){
		Debug.Log("[NPCInteractable] Interact");
		if (enumerator.MoveNext () == false) {
			dialogBand.SetActive (false);
			enumerator = dialogs.GetEnumerator ();
		} else {
			dialogBand.SetActive (true);
			dialogBand.GetComponentInChildren<UnityEngine.UI.Text> ().text = enumerator.Current;
		}
	}

	public void parseTextAsset (TextAsset ft ) {
		string fs = ft.text;
		//string[] fLines = Regex.Split (fs, "\n|\r|\r\n");
		string[] fLines = Regex.Split (fs, "\n");
		for (int i = 0; i < fLines.Length; i++) {
			Debug.Log (fLines [i]);
			dialogs.Add (fLines [i]);
		}
	}
}
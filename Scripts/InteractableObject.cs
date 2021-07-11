using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

enum State
{
    Intro,
    Win,
    Fail,
	Done,
}

public class InteractableObject : MonoBehaviour {

	AudioSource audio;
	public AudioClip audioclip;
	GameObject dialogBand;
	

	State state;

	List<string> dialogs = new List<string>();
	string introDialog;
	string winDialog;
	string failDialog;

	public Color colorBackgroundIntro;
	public Color colorBackgroundWin;
	public Color colorBackgroundFail;
	public TextAsset interactionTextFile;

	public float WinRatePercent = 50;

	// Use this for initialization
	void Start () {
		dialogBand = GameObject.FindWithTag ("DialogBand");
		dialogBand.SetActive(false);
		state = State.Intro;
		parseTextAsset (interactionTextFile);
		
		this.audio = GetComponent<AudioSource>();
		this.audio.loop = false;
		this.audio.clip = audioclip;
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Player player = other.gameObject.GetComponent<Player>();
			player.SetInteractableObject (this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Player player = other.gameObject.GetComponent<Player>();
			player.ClearInteractableObject ();
		}
	}

	void Interact(){
		Debug.Log("[NPCInteractable] Interact");
		if (state == State.Intro) {
			dialogBand.SetActive (true);
			dialogBand.GetComponentInChildren<UnityEngine.UI.Text> ().text = introDialog;
			state = isVictory();
			audio.Play();
			Time.timeScale = 0f;
			dialogBand.GetComponent<Image>().color = colorBackgroundIntro;
		} else  if (state == State.Win){
			dialogBand.SetActive (true);
			dialogBand.GetComponentInChildren<UnityEngine.UI.Text> ().text = winDialog;
			state = State.Done;
			dialogBand.GetComponent<Image>().color = colorBackgroundWin;
		} else  if (state == State.Fail){
			dialogBand.SetActive (true);
			dialogBand.GetComponentInChildren<UnityEngine.UI.Text> ().text = winDialog;
			dialogBand.GetComponent<Image>().color = colorBackgroundFail;
			state = State.Done;
		}
		else{
			Time.timeScale = 1f;
			dialogBand.SetActive (false);
		} 
	}

	State isVictory(){
		if (Random.Range(0f,100f) > WinRatePercent){
			return State.Fail;
		}
		else {
			return State.Win;
		}
	}

	public void parseTextAsset (TextAsset ft ) {
		string fs = ft.text;
		//string[] fLines = Regex.Split (fs, "\n|\r|\r\n");
		string[] fLines = Regex.Split (fs, "\n");
		if (fLines.Length > 0 )
		{
		introDialog = fLines[0];
		}
		if (fLines.Length > 1 )
		{
			winDialog = fLines[1];

		}
		if (fLines.Length > 2 )
		{
			failDialog = fLines[2];
		}
		
	}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class InteractableObject : MonoBehaviour {

	Image eventImage;
	AudioSource audio;
	public Sprite sprite;
	public AudioClip audioclip;
	public float appearanceTimeSeconds = 4f;

	private float timer;

	// Use this for initialization
	void Start () {
		
		this.eventImage = GameObject.FindWithTag("EventImage").GetComponent<Image>();
		this.audio = this.eventImage.GetComponent<AudioSource>();
		this.eventImage.gameObject.SetActive (false);
		this.timer = 0;
	}


	private void Update() {
		if(this.timer > 0f && eventImage.gameObject.activeInHierarchy){
			this.timer -= Time.unscaledDeltaTime; 
		}
		else if (eventImage.gameObject.activeInHierarchy){
			eventImage.gameObject.SetActive (false);
			audio.Stop();
			Time.timeScale = 1;
		}
		
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
			eventImage.gameObject.SetActive (false);
		}
	}

	void Interact(){
		Debug.Log("[NPCInteractable] Interact");

		//Should launch a timer that will deactivate the image and sound
		if (!eventImage.gameObject.activeInHierarchy){
			eventImage.gameObject.SetActive (true);
			eventImage.sprite = sprite;
			this.audio.clip = this.audioclip;
			this.audio.Play();
			this.timer = this.appearanceTimeSeconds;
        	Time.timeScale = 0;
		}
	}

}
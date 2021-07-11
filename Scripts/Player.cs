using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameObject interactableObject;
	private UnityEngine.UI.Text textInteract;
    //Movement
    //Stats
    private Rigidbody2D rig;


    void Start () {
        //get the Rigidbody2D component
        rig = this.transform.GetComponent<Rigidbody2D>();

        textInteract = this.GetComponentInChildren<UnityEngine.UI.Text> ();
		textInteract.enabled = false;
	}
	// Update is called once per frame
	void FixedUpdate () {
        HandleControl();
	}

    void Update(){
		if (Input.GetKeyDown(KeyCode.Return) | Input.GetKeyDown(KeyCode.X))
		{
            if (interactableObject != null)
			{
				interactableObject.SendMessage("Interact");
			}
		}
    }


    void HandleControl()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce(- Vector3.right * 30f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rig.AddForce(Vector3.right * 30f);

        }
        if (Input.GetKey(KeyCode.W))
        {
             rig.AddForce(Vector3.up * 30f);
        }
        if (Input.GetKey(KeyCode.S))
        {
             rig.AddForce(-Vector3.up * 30f);
        }


    }

	public void SetInteractableObject(GameObject interactable){
		this.interactableObject = interactable;
		textInteract.enabled = true;
	}

	public void ClearInteractableObject(){
		this.interactableObject = null;
		textInteract.enabled = false;
	}
}
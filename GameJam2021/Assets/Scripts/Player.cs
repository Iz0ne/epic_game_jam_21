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
        if (Input.GetKeyDown(KeyCode.W))
        {
             rig.AddForce(Vector3.up * 300f);
        }

		if (Input.GetKeyDown(KeyCode.Return))
		{
            if (interactableObject != null)
			{
				interactableObject.SendMessage("Interact");
			}
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
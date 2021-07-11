using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeColilider : MonoBehaviour
{   
    PolygonCollider2D collider;
    BossVision bossVision;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<PolygonCollider2D>();
        this.bossVision = GameObject.FindWithTag("BossVision").GetComponent<BossVision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3[] vertices = bossVision.GetMesh().vertices;
        if(vertices.Length == 0){
            return;
        }
        Vector2[] newVertices = new Vector2[vertices.Length -1];

        Vector3 position = bossVision.GetPosition();

        for(int i=0; i < vertices.Length -1; i++){
            newVertices[i] = vertices[i] - position;
        }

        collider.points = newVertices;
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
			Debug.Log ("Entered Boss vision CollideBox");
		}

	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Exited Boss vision CollideBox");
		}
	}

}

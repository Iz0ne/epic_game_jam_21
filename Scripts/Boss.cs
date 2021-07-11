using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

	GameObject bossVision;

    PolygonCollider2D collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<PolygonCollider2D>();
		bossVision = GameObject.FindWithTag ("BossVision");
		bossVision.SetActive (true);
    }

    // Update is called once per frame
    void Update()
    {
        bossVision.GetComponent<BossVision>().SetPosition(transform.position);
        
    }
}

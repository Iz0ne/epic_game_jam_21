using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : MonoBehaviour
{

	GameObject bossVision;

	// Use this for initialization
	void Start () {
		bossVision = GameObject.FindWithTag ("BossVision");
		bossVision.SetActive (true);
    }

    // Update is called once per frame
    void Update()
    {
        bossVision.GetComponent<BossVision>().SetPosition(transform.position);
        bossVision.GetComponent<BossVision>().SetDirection(new Vector3(1,0));
        
    }
}

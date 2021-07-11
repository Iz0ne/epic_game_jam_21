using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : MonoBehaviour
{

	GameObject bossVision;

    
    AIPath aiPath;

	// Use this for initialization
	void Start () {
        aiPath = GetComponent<AIPath>();
		bossVision = GameObject.FindWithTag ("BossVision");
		bossVision.SetActive (true);
    }

    // Update is called once per frame
    void Update()
    {
        bossVision.GetComponent<BossVision>().SetPosition(transform.position);
        bossVision.GetComponent<BossVision>().SetDirection(aiPath.desiredVelocity);
        
    }
}

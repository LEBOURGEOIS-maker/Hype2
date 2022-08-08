using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour {

    public Vector3 respawnPositions;
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if(other.transform.gameObject.tag == "Player")
        {
            other.transform.position = respawnPositions;
        }
	}
}

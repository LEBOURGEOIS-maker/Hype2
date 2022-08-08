using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    Deathzone deathzone;

	private void Start () {
        deathzone = GameObject.Find("Deathzone").GetComponent<Deathzone>();
	}

    void OnTriggerEnter (Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            deathzone.respawnPositions = gameObject.transform.position;
        }
    }
}

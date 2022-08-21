using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    Animator _doorAnim;

    void Start(){
        _doorAnim = this.transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Collision with player");
            _doorAnim.SetBool("isOpening", true);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Player exit collision area");
            _doorAnim.SetBool("isOpening", false);
        }
    }
}

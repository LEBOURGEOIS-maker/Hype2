using UnityEngine;

public class bridgeScript : MonoBehaviour
{
    public Animator bridgeAnimator;

    void Start()
    {
        bridgeAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        bridgeAnimator.SetTrigger("opening");
        Debug.Log("Le joueur est entré dans la zone du pont.");
    }

    void OnTriggerExit(Collider other)
    {
        bridgeAnimator.SetTrigger("closing");
        Debug.Log("Le joueur a quitté la zone du pont.");
    }
}

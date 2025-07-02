using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    public Animator bridgeAnimator;
    private bool isOpen = false;

    void Start()
    {
        if (bridgeAnimator == null)
            bridgeAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter - IsOpen: " + isOpen);
        if (other.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            Debug.Log("Triggering opening animation");
            bridgeAnimator.SetTrigger("opening");
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            isOpen = false;
            bridgeAnimator.SetTrigger("closing");
            Debug.Log("Le joueur a quitté la zone du pont.");
        }
    }
}

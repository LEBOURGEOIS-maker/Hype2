using UnityEngine;

public class bridgeScript : MonoBehaviour
{
    public Animator bridgeAnimator;
    private bool isOpen = false;

    void Start()
    {
        bridgeAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter function");
        if (!isOpen)
        {
            isOpen = true;
            bridgeAnimator.SetTrigger("opening");
            Debug.Log("Le joueur est entré dans la zone du pont.");
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit function");
        if (isOpen)
        {
            isOpen = false;
            bridgeAnimator.SetTrigger("closing");
            Debug.Log("Le joueur a quitté la zone du pont.");
        }
    }
}

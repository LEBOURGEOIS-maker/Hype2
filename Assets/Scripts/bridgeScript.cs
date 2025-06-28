using UnityEngine;

public class bridgeScript : MonoBehaviour
{
    public Animator bridgeAnimator; // À assigner dans l'inspecteur
    private bool playerInsideZone = false;
    private bool isOpen = false;

    void Update()
    {
        if (playerInsideZone && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpen)
            {
                bridgeAnimator.SetTrigger("opening");
                isOpen = true;
                Debug.Log("Espace appuyé : ouverture du pont.");
            }
            else
            {
                bridgeAnimator.SetTrigger("closing");
                isOpen = false;
                Debug.Log("Espace appuyé : fermeture du pont.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideZone = true;
            Debug.Log("Le joueur est entré dans la zone du pont.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideZone = false;
            Debug.Log("Le joueur a quitté la zone du pont.");
        }
    }
}

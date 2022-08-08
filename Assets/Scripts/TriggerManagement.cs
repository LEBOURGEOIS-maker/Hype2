using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManagement : MonoBehaviour { // nom de classe modifé

	[SerializeField] private LevelLoader levelLoader ;
	// liaison avec de la classe (levelLoader) plutot que la fonction (LoadLevel)  + minuscule sur la variable
	// et faire glisser l'objet en contenant ce script dans l'inspector

	void OnTriggerEnter (Collider other) {
	if(other.transform.gameObject.tag == "Player")
	{
			levelLoader.LoadLevel(0);
			// minuscule sur la variable + appel de la fonction (LoadLevel) présente dans la classe (levelLoader)
	}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestruction : MonoBehaviour {

    public float timer;

	// Use this for initialization by Emilie
	void Start () {
        Destroy(gameObject, timer);
	}
	
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathShowGUI : MonoBehaviour {

    public GameObject prefabGUI;

	void Explode()
    {
        Instantiate(prefabGUI);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GamePreparationManager.instance.OnSceneLoaded();
    }
}

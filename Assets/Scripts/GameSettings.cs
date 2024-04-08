using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

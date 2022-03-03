using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIndicator : MonoBehaviour
{
    public Animation levelTextFade;
    bool alreadyShowedLevelName;
    // Start is called before the first frame update
    void Start()
    {
        alreadyShowedLevelName = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraFollow.showLevel && !alreadyShowedLevelName)
        {
            levelTextFade.Play();
            alreadyShowedLevelName = true;
        }
    }
}

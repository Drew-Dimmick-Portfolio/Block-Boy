using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAvailability : MonoBehaviour
{
    //public LevelSelectorController levelSelector;
    public bool isActive;
    public int levelNumber;
    public Renderer[] cubes;
    public Renderer backdrop;
    public Material cubeMaterialAvailable;
    public Material cubeMaterialUnavailable;
    public Material backDropMaterialAvailable;
    public Material backDropMaterialUnavailable;

    void Start()
    {
        if(levelNumber <= 1)
        {
            isActive = true;
        }
        else
        {
            isActive = LevelAvailabilityHandler.levelsCompleted[levelNumber - 2];
        }
        
        if(isActive)
        {
            foreach(Renderer cube in cubes)
            {
                cube.sharedMaterial = cubeMaterialAvailable;
            }

            backdrop.sharedMaterial = backDropMaterialAvailable;
        }
        else if(!isActive)
        {
            foreach (Renderer cube in cubes)
            {
                cube.sharedMaterial = cubeMaterialUnavailable;
            }

            backdrop.sharedMaterial = backDropMaterialUnavailable;
        }
    }

    void Update()
    {
        
    }
}

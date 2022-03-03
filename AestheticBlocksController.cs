using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AestheticBlocksController : MonoBehaviour
{
    //public LevelSelectorController levelSelector;
    public bool isActive;
    public int lastLevelOfSection;
    public Renderer[] cubes;
    public Material cubeMaterialAvailable;
    public Material cubeMaterialUnavailable;

    void Start()
    {
        
        isActive = LevelAvailabilityHandler.levelsCompleted[lastLevelOfSection - 1];

        if (isActive)
        {
            foreach (Renderer cube in cubes)
            {
                cube.sharedMaterial = cubeMaterialAvailable;
            }
        }
        else if (!isActive)
        {
            foreach (Renderer cube in cubes)
            {
                cube.sharedMaterial = cubeMaterialUnavailable;
            }
        }
    }

    void Update()
    {

    }
}

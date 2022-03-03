using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    public Material[] player;

    private Color32 colorOption01 = new Color32(186, 122, 221, 255);
    private Color32 colorOption02 = new Color32(0, 255, 255, 255);
    private Color32 colorOption03 = new Color32(255, 235, 0, 255);
    private Color32 colorOption04 = new Color32(255, 119, 0, 255);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MenuButtons.colorValue == 1)
        {
            player[0].SetColor("_BaseColor", colorOption01);
            player[1].SetColor("_BaseColor", colorOption01);
            player[2].SetColor("_BaseColor", colorOption01);
        }
        else if (MenuButtons.colorValue == 2)
        {
            player[0].SetColor("_BaseColor", colorOption02);
            player[1].SetColor("_BaseColor", colorOption02);
            player[2].SetColor("_BaseColor", colorOption02);
        }
        else if(MenuButtons.colorValue == 3)
        {
            player[0].SetColor("_BaseColor", colorOption03);
            player[1].SetColor("_BaseColor", colorOption03);
            player[2].SetColor("_BaseColor", colorOption03);
        }
        else if (MenuButtons.colorValue == 4)
        {
            player[0].SetColor("_BaseColor", colorOption04);
            player[1].SetColor("_BaseColor", colorOption04);
            player[2].SetColor("_BaseColor", colorOption04);
        }
    }
}

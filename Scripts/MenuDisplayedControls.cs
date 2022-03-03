using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDisplayedControls : MonoBehaviour
{
    public Material[] material;
    public Renderer[] keys;
    public TextMeshPro[] keyText;
    private Color32 pressedKeyTextColor = new Color32(255, 255, 255, 255);
    private Color32 originalKeyTextColor = new Color32(0, 0, 0, 255);

    void Start()
    {
       foreach(Renderer rend in keys)
        {
            rend.sharedMaterial = material[0];
        }

        foreach (TextMeshPro tmp in keyText)
        {
            tmp.color = originalKeyTextColor;
        }
    }

    void Update()
    {
        CheckForKeyDown();
        CheckForKeyRelease();
    }

    private void CheckForKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keys[4].sharedMaterial = material[1];
            //keyText[4].color = pressedKeyTextColor;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keys[0].sharedMaterial = material[1];
            //keyText[0].color = pressedKeyTextColor;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            keys[1].sharedMaterial = material[1];
            //keyText[1].color = pressedKeyTextColor;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            keys[2].sharedMaterial = material[1];
            //keyText[2].color = pressedKeyTextColor;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            keys[3].sharedMaterial = material[1];
            //keyText[3].color = pressedKeyTextColor;
        }
    }

    private void CheckForKeyRelease()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            keys[4].sharedMaterial = material[0];
            keyText[4].color = originalKeyTextColor;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            keys[0].sharedMaterial = material[0];
            keyText[0].color = originalKeyTextColor;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            keys[1].sharedMaterial = material[0];
            keyText[1].color = originalKeyTextColor;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            keys[2].sharedMaterial = material[0];
            keyText[2].color = originalKeyTextColor;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            keys[3].sharedMaterial = material[0];
            keyText[3].color = originalKeyTextColor;
        }
    }
}

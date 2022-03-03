using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject wall;
    public Material coreMaterial;
    public Material transparentCore;
    public Material tile1;
    public Material tile2;
    public Material tile3;
    static public bool fadeOut;
    static public bool changeCullingMask;
    float tileAlpha;

    void Start()
    {
        fadeOut = false;
        changeCullingMask = false;
        AlphaController.alpha = 1;

        wall.GetComponent<MeshRenderer>().material = coreMaterial;
        ResetAlpha(transparentCore);
        ResetAlpha(tile1);
        ResetAlpha(tile2);
        ResetAlpha(tile3);

        tile1.mainTextureScale = new Vector2(wall.transform.localScale.x, wall.transform.localScale.y);
        tile2.mainTextureScale = new Vector2(wall.transform.localScale.x, wall.transform.localScale.z);
        tile3.mainTextureScale = new Vector2(wall.transform.localScale.z, wall.transform.localScale.y);
    }

    private void Update()
    {
        if(CameraFollow.moveCameraToEnd)
        {
            fadeOut = true;
        }

        if(fadeOut)
        {
            //wall.GetComponent<MeshRenderer>().material = transparentCore;
            //Debug.Log("Fading Out..");
            //FadeOut(transparentCore, wall);
            //FadeOut(tile1, wall);
            //FadeOut(tile2, wall);
            //FadeOut(tile3, wall);
        }
    }
    void ResetAlpha(Material tile)
    {
        Color tempColor = tile.color;
        tempColor.a = AlphaController.alpha;
        tile.color = tempColor;
    }

    static public void FadeOut(Material tile, GameObject obj)
    {
        Color newAlpha = tile.color;
        newAlpha.a = AlphaController.alpha;

        if(newAlpha.a <= 0)//newAlpha.a <= 0)
        {
            newAlpha.a = AlphaController.alpha;
            obj.SetActive(false);
            fadeOut = false;
            changeCullingMask = true;
        }
        tile.color = newAlpha;
        //tile.SetColor("_BaseColor", newAlpha);
    }
}

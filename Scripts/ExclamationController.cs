using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public GameObject xMark;
    public Material exclamationMark;

    bool resetAlpha;

    void Start()
    {
        resetAlpha = true;
        RemoveAlpha(exclamationMark);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f + (0.5f * playerController.numTallMunchiesCollected), player.transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1f + (0.5f * playerController.numTallMunchiesCollected), player.transform.position.z);

        if(CameraFollow.finishedLevel && !WallController.fadeOut)
        {
            ResetAlpha(exclamationMark);
        }
        else if(WallController.fadeOut)
        {
            WallController.FadeOut(exclamationMark, xMark);
        }
    }

    void RemoveAlpha(Material tile)
    {
        Color tempColor = tile.color;
        tempColor.a = 0;
        tile.color = tempColor;
    }

    void ResetAlpha(Material tile)
    {
        if(resetAlpha)
        {
            Color tempColor = tile.color;
            tempColor.a = 1;
            tile.color = tempColor;
            resetAlpha = false;
        }
    }
}

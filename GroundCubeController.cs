using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCubeController : MonoBehaviour
{
    public Material tile;

    void Start()
    {
        tile.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
    }
}

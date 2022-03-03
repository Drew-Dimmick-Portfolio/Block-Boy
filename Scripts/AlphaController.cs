using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaController : MonoBehaviour
{
    static public float alpha;
    float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 1;
        fadeSpeed = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if(WallController.fadeOut)
        {
            float tempAlpha = alpha;
            if ((tempAlpha - (fadeSpeed * Time.deltaTime)) <= 0)
            {
                alpha = 0;
            }
            else
            {
                alpha -= fadeSpeed * Time.deltaTime;
            }
        }
    }
}

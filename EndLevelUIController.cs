using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelUIController : MonoBehaviour
{

    [SerializeField]
    CanvasGroup uiCanvasGroup;
    float fadeSpeed = 3;

    private void Start()
    {
        uiCanvasGroup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(CameraFollow.showUI && uiCanvasGroup.alpha < 1)
        {
            uiCanvasGroup.gameObject.SetActive(true);
            uiCanvasGroup.alpha += fadeSpeed * Time.deltaTime;
        }
    }

    public void LoadLevelSelector()
    {
        SceneManager.LoadScene("Level Selector 1", LoadSceneMode.Single);
    }

}

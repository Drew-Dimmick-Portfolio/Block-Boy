using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Camera camera;
    public Transform player;
    public Transform end;
    private float smoothSpeed = 0.25f;
    public Vector3 offset;
    public bool transitionFlag = false;
    float delayAfterLevelComplete = 1.5f;
    float levelCompleteDelayTimer;

    Vector3 playerPosition;

    static public string levelName;

    static public bool showUI;

    public bool inLevelSelector;

    static public bool showLevel;

    static public bool finishedLevel;
    static public bool moveCameraToEnd;

    public Vector3 desiredPosition;
    public Vector3 smoothedPosition;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        if(inLevelSelector)
        {
            Load();
        }

        showLevel = false;
        showUI = false;
        moveCameraToEnd = false;
        levelCompleteDelayTimer = 0;
        finishedLevel = false;
        if(inLevelSelector)
        {
            Vector3 startiingPos = new Vector3(player.transform.position.x, 20f, player.transform.position.z);
            transform.position = startiingPos + offset;
        }
    }

    private void Update()
    {
        if (finishedLevel && moveCameraToEnd)
        {
            RepositionCamera(end, smoothSpeed * 3);
        }
        else if (LevelSelectorController.levelSelected)
        {
            RepositionCamera(end, smoothSpeed);
        }
        else
        {
            RepositionCamera(player, smoothSpeed);
        }

        if (transform.position.y >= 1f)
        {
            showLevel = true;
        }
        if(finishedLevel)
        {
            if(WallController.changeCullingMask)
            {
                //camera.cullingMask = 1 << LayerMask.NameToLayer("Player");
            }

            if (levelCompleteDelayTimer >= delayAfterLevelComplete)
            {
                levelCompleteDelayTimer = 0;
                moveCameraToEnd = true;
            }

            levelCompleteDelayTimer += Time.deltaTime;
        }

        if(showUI)
        {
            Debug.Log("Showing UI");
        }
    }

    void RepositionCamera(Transform target, float speed)
    {
        if (!transitionFlag)
        {
            //smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, speed);
        }
        desiredPosition = target.position + offset;

        transform.position = smoothedPosition;

        /*if(Vector3.Distance(transform.position, desiredPosition) < .01f)
        {
            transform.position = desiredPosition;
            //transitionFlag = true;
        }*/


        if (moveCameraToEnd)
        {
            if (Vector3.Distance(transform.position, desiredPosition) < 0.5f)
            {
                showUI = true;
            }
            if (Vector3.Distance(transform.position, desiredPosition) < 0.25f)
            {
                desiredPosition = transform.position;
            }
        }
        else if(LevelSelectorController.levelSelected)
        {
            if (Vector3.Distance(transform.position, desiredPosition) < 0.5f)
            {
                Save();
                LevelSelectorController.levelSelected = false;
                SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            }
        }
    }

    public void Save()
    {
        playerPosition = player.transform.position;
        ES3.Save("playerPosition", playerPosition, "levelInfo.es3");
    }

    public void Load()
    {
        if (ES3.KeyExists("playerPosition", "levelInfo.es3"))
        {
            playerPosition = ES3.Load<Vector3>("playerPosition", "levelInfo.es3");
            player.transform.position = playerPosition;
        }
    }
}

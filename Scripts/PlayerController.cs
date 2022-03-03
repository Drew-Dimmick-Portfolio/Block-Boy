using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 6f;
    float snapDistance = 0.25f;
    float rayOffsetX;
    [SerializeField]
    float rayOffsetY;
    float rayOffsetZ;

    Vector3 xOffset;
    Vector3 yOffset;
    Vector3 zOffset;
    Vector3 zAxisOriginA;
    Vector3 zAxisOriginB;
    Vector3 xAxisOriginA;
    Vector3 xAxisOriginB;

    Vector3 targetPosition;
    Vector3 startingPosition;
    Vector3 targetRotation;
    Vector3 startingRotation;
    bool moving;
    bool pauseMovement;

    RaycastHit hit;
    float rayLengthX;
    float rayLengthZ;

    bool showResetTutorial;
    public TextMeshProUGUI resetTutorial;
    float tutorialTimer;
    float tutorialFadeTime = 1;
    bool fadeOutTutorial;
    bool showingTutorial;
    Color currentTutorialColor;

    KeyCode lastKeyPressed;
    Vector3 playerDirection;

    [SerializeField]
    float autoMoveLimit = 0.15f;
    float autoMoveTimer;

    string munchieWallPosition;

    public int numMunchiesRequired;
    int totalMunchiesCollected;
    public int numTutorialMunchies;
    List<GameObject> munchieGameObjectList;

    string munchieCollected;
    Vector3 restorePosition;
    Vector3 spawnPosition;

    public int numTallMunchiesCollected;

    public CameraFollow camera;
    public Material playerMaterial1;
    public Material playerMaterial2;
    public Material playerMaterial3;

    public AudioSource sfxController;
    public AudioClip blockCollected;

    private int heightLimit = 11;
    public float smoothSpeed = 0.125f;
    public float fallSpeed = 1.25f;
    public bool inMenu = false;
    public string loadLevel;
    public LevelAvailabilityHandler levelAvailability;
    public GameObject player;
    //public GameObject levelEnd;
    //public GameObject player;
    Vector3 playerSize = new Vector3(1f, 1f, 1f);

    private void Start()
    {
        showingTutorial = false;
        fadeOutTutorial = false;
        tutorialTimer = 0;
        showResetTutorial = false;
        rayOffsetX = 0.45f;
        rayOffsetY = 0.45f;
        rayOffsetZ = 0.45f;
        rayLengthX = 1.45f;
        rayLengthZ = 1.45f;
        numTallMunchiesCollected = 0;
        autoMoveTimer = 0;
        pauseMovement = false;
        spawnPosition = player.transform.position;
        totalMunchiesCollected = 0;

        munchieGameObjectList = new List<GameObject>();

        playerMaterial1.mainTextureScale = new Vector2(1f, 1f);
        playerMaterial2.mainTextureScale = new Vector2(1f, 1f);
        playerMaterial3.mainTextureScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        yOffset = transform.position + Vector3.up * rayOffsetY;
        zOffset = Vector3.forward * rayOffsetZ;
        xOffset = Vector3.right * rayOffsetX;

        zAxisOriginA = yOffset + xOffset;
        zAxisOriginB = yOffset - xOffset;

        xAxisOriginA = yOffset + zOffset;
        xAxisOriginB = yOffset - zOffset;

        // Draw Debug Rays

        Debug.DrawLine(
                zAxisOriginA,
                zAxisOriginA + Vector3.forward * rayLengthZ,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                zAxisOriginB,
                zAxisOriginB + Vector3.forward * rayLengthZ,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                zAxisOriginA,
                zAxisOriginA + Vector3.back * rayLengthZ,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                zAxisOriginB,
                zAxisOriginB + Vector3.back * rayLengthZ,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                xAxisOriginA,
                xAxisOriginA + Vector3.left * rayLengthX,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xAxisOriginB,
                xAxisOriginB + Vector3.left * rayLengthX,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                xAxisOriginA,
                xAxisOriginA + Vector3.right * rayLengthX,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xAxisOriginB,
                xAxisOriginB + Vector3.right * rayLengthX,
                Color.red,
                Time.deltaTime);

        CheckForReset();

        if(totalMunchiesCollected == numMunchiesRequired)
        {
            CameraFollow.finishedLevel = true;
            levelAvailability.levelCompleted = true;
            //WallController.fadeOut = true;
        }

        if(pauseMovement)
        {
            CheckForCollectables();
        }

        if (moving && !pauseMovement && !CameraFollow.finishedLevel)
        {
            if(Vector3.Distance(startingPosition, player.transform.position) > 1f)
            {
                player.transform.position = targetPosition;
                moving = false;
                pauseMovement = true;
                //playerMovement.Play();

                return;
            }

            player.transform.position += (targetPosition - startingPosition) * moveSpeed * Time.deltaTime;
            return;
        }

        if (autoMoveTimer >= autoMoveLimit)
        {
            pauseMovement = false;
            autoMoveTimer = 0f;
        }

        if (pauseMovement)
        {
            autoMoveTimer += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if(CanMove(Vector3.forward)) {
                playerDirection = Vector3.forward;
                targetPosition = player.transform.position + new Vector3(0f, 0f, 1f);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x * 10.0f) * 0.1f, Mathf.Round(targetPosition.y * 10.0f) * 0.1f, Mathf.Round(targetPosition.z * 10.0f) * 0.1f);
                startingPosition = player.transform.position;
                moving = true;
                lastKeyPressed = KeyCode.W;
            }
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            moving = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(CanMove(Vector3.left))
            {
                playerDirection = Vector3.left;
                targetPosition = player.transform.position + new Vector3(-1f, 0f, 0f);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x * 10.0f) * 0.1f, Mathf.Round(targetPosition.y * 10.0f) * 0.1f, Mathf.Round(targetPosition.z * 10.0f) * 0.1f);
                startingPosition = player.transform.position;
                moving = true;
                lastKeyPressed = KeyCode.A;
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            moving = false;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if(CanMove(Vector3.back))
            {
                playerDirection = Vector3.back;
                targetPosition = player.transform.position + new Vector3(0f, 0f, -1f);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x * 10.0f) * 0.1f, Mathf.Round(targetPosition.y * 10.0f) * 0.1f, Mathf.Round(targetPosition.z * 10.0f) * 0.1f);
                startingPosition = player.transform.position;
                moving = true;
                lastKeyPressed = KeyCode.S;
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            moving = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if(CanMove(Vector3.right))
            {
                playerDirection = Vector3.right;
                targetPosition = player.transform.position + new Vector3(1f, 0f, 0f);
                targetPosition = new Vector3(Mathf.Round(targetPosition.x * 10.0f) * 0.1f, Mathf.Round(targetPosition.y * 10.0f) * 0.1f, Mathf.Round(targetPosition.z * 10.0f) * 0.1f);
                startingPosition = player.transform.position;
                moving = true;
                lastKeyPressed = KeyCode.D;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            moving = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Test")
        {
            Debug.Log("Test Working");
        }
        if (col.tag == "Wide Munchie")
        {
            munchieCollected = "wide";
            munchieWallPosition = col.gameObject.GetComponent<WideMunchieController>().wallPosition;
            if(totalMunchiesCollected == numTutorialMunchies)
            {
                showResetTutorial = col.gameObject.GetComponent<WideMunchieController>().incorrectMunchieTutorial;
            }
            col.gameObject.SetActive(false);
            munchieGameObjectList.Add(col.gameObject);
        }
        else if (col.tag == "Tall Munchie")
        {
            munchieCollected = "tall";
            if (totalMunchiesCollected == numTutorialMunchies)
            {
                showResetTutorial = col.gameObject.GetComponent<TallMunchieController>().incorrectMunchieTutorial;
            }
            col.gameObject.SetActive(false);
            munchieGameObjectList.Add(col.gameObject);
        }
        else if (col.tag == "Long Munchie")
        {
            munchieCollected = "long";
            munchieWallPosition = col.gameObject.GetComponent<LongMunchieController>().wallPosition;
            if (totalMunchiesCollected == numTutorialMunchies)
            {
                showResetTutorial = col.gameObject.GetComponent<LongMunchieController>().incorrectMunchieTutorial;
            }
            col.gameObject.SetActive(false);
            munchieGameObjectList.Add(col.gameObject);
        }
        else if(col.tag == "Restore Munchie")
        {
            restorePosition = col.gameObject.transform.position;
            munchieCollected = "restore";
            col.gameObject.SetActive(false);
            munchieGameObjectList.Add(col.gameObject);
        }
    }

    void ElongatePlayer()
    {
        if (CanMove(Vector3.forward))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.5f);
            targetPosition.z += 0.5f;
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 0.5f);
            targetPosition.z -= 0.5f;
        }

        Debug.Log("Longer");
        Vector3 newSize = new Vector3(player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z + 1.0f);
        player.transform.localScale = newSize;
        ResizeTextures();
        rayOffsetZ = player.transform.localScale.z / 2 - 0.05f;
        rayLengthZ += 0.5f;

        totalMunchiesCollected++;
        //sfxController.PlayOneShot(blockCollected);
    }

    void AddPlayerHeight()
    {
        if (playerSize.y < heightLimit)
        {
            Debug.Log("Taller");
            Vector3 newSize = new Vector3(player.transform.localScale.x, player.transform.localScale.y + 1.0f, player.transform.localScale.z);
            player.transform.position += new Vector3(0f, 0.5f, 0f);
            player.transform.localScale = newSize;
            ResizeTextures();
            rayOffsetY -= 0.5f;
            numTallMunchiesCollected++;
            //levelEnd.transform.position = new Vector3(levelEnd.transform.position.x, levelEnd.transform.position.y / numTallMunchiesCollected, levelEnd.transform.position.z);

            totalMunchiesCollected++;
            //sfxController.PlayOneShot(blockCollected);
        }
    }

    void WidenPlayer()
    {
        if (CanMove(Vector3.right))
        {
            player.transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y, player.transform.position.z);
            targetPosition.x += 0.5f;
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
            targetPosition.x -= 0.5f;
        }

        Debug.Log("WIDER");
        Vector3 newSize = new Vector3(player.transform.localScale.x + 1.0f, player.transform.localScale.y, player.transform.localScale.z);
        player.transform.localScale = newSize;
        ResizeTextures();        
        rayOffsetX = player.transform.localScale.x / 2 - 0.05f;
        rayLengthX += 0.5f;

        totalMunchiesCollected++;
        //sfxController.PlayOneShot(blockCollected);
    }
    void RestorePlayer()
    {
        player.transform.position = restorePosition;
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        rayOffsetX = 0.45f;
        rayOffsetY = 0.45f;
        rayOffsetZ = 0.45f;
        rayLengthX = 1.45f;
        rayLengthZ = 1.45f;
        numTallMunchiesCollected = 0;
        ResizeTextures();

        //levelEnd.transform.position = new Vector3(levelEnd.transform.position.x, 3.5f, levelEnd.transform.position.z);
        totalMunchiesCollected++;
        //sfxController.PlayOneShot(blockCollected);
    }

    void ResizeTextures()
    {
        playerMaterial1.mainTextureScale = new Vector2(player.transform.localScale.z, player.transform.localScale.y);
        playerMaterial2.mainTextureScale = new Vector2(player.transform.localScale.x, player.transform.localScale.z);
        playerMaterial3.mainTextureScale = new Vector2(player.transform.localScale.x, player.transform.localScale.y);
    }

    void CheckForCollectables()
    {
        switch (munchieCollected)
        {
            case "long":
                ElongatePlayer();
                munchieCollected = null;

                break;
            case "wide":
                WidenPlayer();
                munchieCollected = null;
                break;
            case "tall":
                AddPlayerHeight();
                munchieCollected = null;
                break;
            case "restore":
                RestorePlayer();
                munchieCollected = null;
                break;
        }
    }

    void FlipRayCasts()
    {
        float tempRayOffset;
        float tempRayLength;

        tempRayOffset = rayOffsetX;
        tempRayLength = rayLengthX;

        rayOffsetX = rayOffsetZ;
        rayLengthX = rayLengthZ;

        rayOffsetZ = tempRayOffset;
        rayLengthZ = tempRayLength;
    }
    void CheckForReset()
    {
        if(showResetTutorial)
        {
            currentTutorialColor = Color.clear;
            FadeInTutorialText(currentTutorialColor);
        }
        else if(fadeOutTutorial && showingTutorial)
        {
            currentTutorialColor = Color.white;
            FadeOutTutorialText(currentTutorialColor);
        }
        if (Input.GetKey(KeyCode.R) && !CameraFollow.finishedLevel)
        {
            ReloadPlayer();
        }
    }

    void ReloadPlayer()
    {
        player.transform.position = spawnPosition;
        ResetPlayer(spawnPosition);
    }

    void ResetPlayer(Vector3 restorePosition)
    {
        player.transform.position = restorePosition;
        player.transform.localScale = new Vector3(1f, 1f, 1f);
        rayOffsetX = 0.45f;
        rayOffsetY = 0.45f;
        rayOffsetZ = 0.45f;
        rayLengthX = 1.45f;
        rayLengthZ = 1.45f;
        tutorialTimer = 0;
        numTallMunchiesCollected = 0;
        totalMunchiesCollected = 0;
        showResetTutorial = false;
        fadeOutTutorial = true;
        ResizeTextures();

        foreach (GameObject munchie in munchieGameObjectList)
        {
            munchie.SetActive(true);
        }
    }

    void FadeInTutorialText(Color startingColor)
    {
        showingTutorial = true; 

        if (tutorialTimer < 1)
        {
            resetTutorial.color = Color.Lerp(startingColor, Color.white, tutorialTimer);
        }

        tutorialTimer += Time.deltaTime * 2;
    }

    void FadeOutTutorialText(Color startingColor)
    {
        if (tutorialTimer < 1)
        {
            resetTutorial.color = Color.Lerp(startingColor, Color.clear, tutorialTimer);
        }
        else
        {
            fadeOutTutorial = false;
            showingTutorial = false;
            tutorialTimer = 0;
        }

        tutorialTimer += Time.deltaTime * 3;
    }

    bool CanMove(Vector3 direction)
    {
        if (Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        {
            for(int i = 0; i <= numTallMunchiesCollected; i++)
            {
                if (HittingWall(direction, Vector3.right, rayOffsetX, rayLengthZ, rayOffsetY + i))
                {
                    return false;
                }
            }
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            for (int i = 0; i <= numTallMunchiesCollected; i++)
            {
                if (HittingWall(direction, Vector3.forward, rayOffsetZ, rayLengthX, rayOffsetY + i))
                {
                    return false;
                }
            }
        }

        return true;
    }

    bool HittingWall(Vector3 direction, Vector3 axis, float rayOffset, float rayLength, float yRayOffset)
    {
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset + axis * rayOffset, direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset + axis * (rayOffset * 3 / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset + axis * (rayOffset * 2 / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset + axis * (rayOffset / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;

        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset - axis * rayOffset, direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset - axis * (rayOffset * 3 / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset - axis * (rayOffset * 2 / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        if (Physics.Raycast(player.transform.position + Vector3.up * yRayOffset - axis * (rayOffset / 4), direction, out hit, rayLength) && hit.transform.tag == "wall")
            return true;
        return false;
    }
}

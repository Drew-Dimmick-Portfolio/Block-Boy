using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelectorController : MonoBehaviour
{
    public string levelName;
    public int levelNumber;
    static public bool levelSelected;
    bool playerDetected;
    bool playSelectSound;

    public AudioSource selectSoundSource;
    public AudioClip selectSoundClip;

    public LevelAvailability levelAvailability;

    public Animator levelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        playSelectSound = false;
        levelSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerDetected && levelAvailability.isActive)
        {
            levelSelected = true;
            CameraFollow.levelName = levelName;
            //SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }

        if(playerDetected && playSelectSound && !selectSoundSource.isPlaying)
        {
            playSelectSound = false;
            selectSoundSource.PlayOneShot(selectSoundClip);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            playerDetected = true;
            playSelectSound = true;

            levelAnimator.SetBool("LevelSelected", true);
            //show SPACE ui
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            playerDetected = false;
            levelAnimator.SetBool("LevelSelected", false);
            //remove SPACE ui
        }
    }
}

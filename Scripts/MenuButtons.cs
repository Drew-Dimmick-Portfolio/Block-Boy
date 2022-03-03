using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Renderer button;
    public Material[] material;
    public TextMeshPro tmpText;
    public TextMeshPro tmpText2;
    
    static public int colorValue;
    public string menuButton;
    public string buttonAction;
    private bool onButtonFlag = false;
    private Color32 originalColor = new Color32(79, 53, 125, 188); 
    private Color32 selectColor = new Color32(0, 255, 29, 189); 
    private Color32 originalTextColor = new Color32(45, 20, 80, 255);
    private Color32 hoverTextColor = new Color32(255, 210, 227, 255);
    private Color32 selectTextColor = new Color32(197, 255, 173, 255);

    public Animator menu01;
    public Animator menu02;

    public AudioSource selectSoundSource;
    public AudioClip selectSoundClip;
    bool playerDetected;
    bool playSelectSound;
    //Renderer rend;

    void Start()
    {
        Load();
        //musicVolume = 5f;
        //colorValue = 1;
        button.sharedMaterial = material[0];
        playerDetected = false;
        playSelectSound = false;

        material[1].SetColor("_Color", originalColor);
        tmpText.color = originalTextColor;
        tmpText2.color = originalTextColor;
    }

    void Update()
    {
        if(menuButton == "music")
        {
            if (MusicLoopController.musicVolume == 0)
            {
                tmpText2.text = MusicLoopController.musicVolume + ">";
            }
            else if (MusicLoopController.musicVolume == 10)
            {
                tmpText2.text = "<" + MusicLoopController.musicVolume + "\u2007";
            }
            else
            {
                tmpText2.text = "<" + MusicLoopController.musicVolume + ">";
            }
        }
        else if(menuButton == "color")
        {
            if (colorValue == 1)
            {
                tmpText2.text = colorValue + ">";
            }
            else if (colorValue == 4)
            {
                tmpText2.text = "<" + colorValue + "\u2007";
            }
            else
            {
                tmpText2.text = "<" + colorValue + ">";
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(buttonAction)
            {
                case "load game":
                    Save();
                    SceneManager.LoadScene("Level Selector 1", LoadSceneMode.Single);
                    break;
                case "new game":
                    Save();
                    ES3.DeleteFile("levelInfo.es3");
                    SceneManager.LoadScene("Level Selector 1", LoadSceneMode.Single);
                    break;
                case "settings":
                    menu01.SetInteger("menuState", 2);
                    menu02.SetInteger("menuState", 2);
                    break;
                case "back":
                    Debug.Log("Back button");
                    menu01.SetInteger("menuState", 1);
                    menu02.SetInteger("menuState", 1);
                    break;
                case "quit":
                    Save();
                    Application.Quit();
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            switch(buttonAction)
            {
                case "color":
                    if (colorValue > 1)
                    {
                        colorValue--;
                        //tmpText2.text = "<" + colorValue + ">";
                    }
                    break;
                case "music":
                    if (MusicLoopController.musicVolume > 0)
                    {
                        MusicLoopController.musicVolume--;
                        //tmpText2.text = "<" + musicVolume + ">";
                    }
               
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            switch (buttonAction)
            {
                case "color":
                    if (colorValue < 4)
                    {
                        colorValue++;
                        //tmpText2.text = "<" + colorValue + ">";
                    }
                    break;
                case "music":
                    if (MusicLoopController.musicVolume < 10)
                    {
                        MusicLoopController.musicVolume++;
                        //tmpText2.text = "<" + musicVolume + ">";
                    }

                    break;
            }
        }

        if (playerDetected && playSelectSound && !selectSoundSource.isPlaying)
        {
            playSelectSound = false;
            selectSoundSource.PlayOneShot(selectSoundClip);
        }
    }

    public void Save()
    {
        ES3.Save("music", MusicLoopController.musicVolume, "menuInfo.es3");
        ES3.Save("color", colorValue, "menuInfo.es3");
    }

    public void Load()
    {
        if (ES3.FileExists("menuInfo.es3"))
        {
            colorValue = ES3.Load<int>("color", "menuInfo.es3");
        }
        else
        {
            colorValue = 1;
        }

        //ES3.LoadInto("levels", levelsCompleted);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            playerDetected = true;
            playSelectSound = true;
            button.sharedMaterial = material[1];
            onButtonFlag = true;
            buttonAction = menuButton;
            tmpText.color = hoverTextColor;
            tmpText2.color = hoverTextColor;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            playerDetected = false;
            button.sharedMaterial = material[0];
            onButtonFlag = false;
            buttonAction = null;
            tmpText.color = originalTextColor;
            tmpText2.color = originalTextColor;

            material[1].SetColor("_Color", originalColor);
        }
    }
}

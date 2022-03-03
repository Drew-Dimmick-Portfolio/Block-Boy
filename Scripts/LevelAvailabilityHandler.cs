using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAvailabilityHandler : MonoBehaviour
{
    
    static public bool[] levelsCompleted;
    [SerializeField]
    public int currentLevel = 1;
    public bool levelCompleted;
    bool saved;
    void Start()
    {
        saved = false;
        levelsCompleted = new bool[15];
        if (ES3.FileExists("levelInfo.es3"))
        {
            Load();
        }
    }

    public void Save()
    {
        ES3.Save("levels", levelsCompleted, "levelInfo.es3");
    }

    public void Load()
    {
        if(ES3.FileExists("levelInfo.es3"))
        {
            levelsCompleted = ES3.Load<bool[]>("levels", "levelInfo.es3");
        }

        //ES3.LoadInto("levels", levelsCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelCompleted && !saved)
        {
            levelsCompleted[currentLevel - 1] = true;
            Save();
            saved = true;
        }
    }
}

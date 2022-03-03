using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoopController : MonoBehaviour
{
    static public float musicVolume;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    private bool isCurrentAudioSource = true;
    public AudioSource currentAudioSource { get { if (isCurrentAudioSource) { return audioSource1; } else { return audioSource2; } } }
    public AudioSource otherAudioSource { get { if (isCurrentAudioSource) { return audioSource2; } else { return audioSource1; } } }

    public float loadingTimeBeforeClipEnds;

    double dspTimeOfNextEvent;
    double timeDelay;

    bool isLooping = false;

    private void Start()
    {
        Load();
    }

    public void Update()
    {
        audioSource1.volume = musicVolume / 10;
        audioSource2.volume = musicVolume / 10;

        if (!currentAudioSource.isPlaying)
            isLooping = false;


        if (!isLooping
           && currentAudioSource.isPlaying
           && currentAudioSource.time >= currentAudioSource.clip.length - loadingTimeBeforeClipEnds)
        {
            isLooping = true;

            StartCoroutine(Loop());
            //Loop();
        }
        else
            isLooping = false;
    }

    public void Save()
    {
        ES3.Save("music", musicVolume, "menuInfo.es3");
    }

    public void Load()
    {
        if (ES3.FileExists("menuInfo.es3"))
        {
            musicVolume = ES3.Load<float>("music", "menuInfo.es3");
        }
        else
        {
            musicVolume = 5;
        }
    }


    public IEnumerator Loop()
    {
        //otherAudioSource.PlayDelayed(timeDelay);  //
        otherAudioSource.PlayScheduled(dspTimeOfNextEvent);

        dspTimeOfNextEvent = dspTimeOfNextEvent + otherAudioSource.clip.length;

        timeDelay = dspTimeOfNextEvent - AudioSettings.dspTime;

        isCurrentAudioSource = !isCurrentAudioSource;

        yield return new WaitForSecondsRealtime((float)timeDelay);

        //otherAudioSource.Stop();  //

        isLooping = false;
    }



    //ui button handler
    public void StartLoop()
    {
        timeDelay = 1f;
        dspTimeOfNextEvent = AudioSettings.dspTime + timeDelay;

        currentAudioSource.PlayScheduled(dspTimeOfNextEvent);

        dspTimeOfNextEvent = dspTimeOfNextEvent + currentAudioSource.clip.length;
    }
}

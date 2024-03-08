using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicsManager : MonoBehaviour
{
    private static CinematicsManager _instance;

    [SerializeField]
    private PlayableDirector playableDirector;
    [SerializeField]
    private CinematicsDatabase cinematicsDatabase; // Assign this asset to change cinematics

    #region
    public static CinematicsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CinematicsManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("CinematicsManager");
                    _instance = singletonObject.AddComponent<CinematicsManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // If the PlayableDirector is not set in the Inspector, try to get it from the GameObject
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
    }

    // Function to play a specific PlayableAsset
    public void PlayTimeline(PlayableAsset timelineAsset)
    {
        if (timelineAsset != null)
        {
            playableDirector.playableAsset = timelineAsset;
            playableDirector.Play();
        }
        else
        {
            Debug.LogError("Timeline Asset is null!");
        }
    }

    // Function to play a cinematic based on cinematic ID
    public void PlayCinematic(string cinematicID)
    {
        PlayableAsset timelineAsset = cinematicsDatabase.GetCinematic(cinematicID);
        if (timelineAsset != null)
        {
            PlayTimeline(timelineAsset);
        }
        else
        {
            Debug.LogError("Cinematic with ID " + cinematicID + " not found in the database.");
        }
    }

    // Function to stop the current timeline playback
    public void StopTimeline()
    {
        playableDirector.Stop();
    }

    // Function to pause the current timeline playback
    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    // Function to resume the paused timeline playback
    public void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    // Function to set the time of the current timeline playback
    public void SetTimelineTime(double time)
    {
        playableDirector.time = time;
    }

    // Function to get the current time of the timeline playback
    public double GetTimelineTime()
    {
        return playableDirector.time;
    }

    // Function to check if the timeline is currently playing
    public bool IsTimelinePlaying()
    {
        return playableDirector.state == PlayState.Playing;
    }

    // Function to check if the timeline is currently paused
    public bool IsTimelinePaused()
    {
        return playableDirector.state == PlayState.Paused;
    }
}

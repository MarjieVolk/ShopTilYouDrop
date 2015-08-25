using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System;

public class AudioSwitcher : MonoBehaviour
{
    private int numLocks = 0;

    public AudioMixerSnapshot levelMusic;
    public AudioSource LevelMusicSource;
    public AudioMixerSnapshot pauseMusic;
    public AudioSource PauseMusicSource;
    public float TransitionSeconds;
    public float TimeWarpingFactor;

    private AudioMixerSnapshot current;

    private void Pause()
    {
        Time.timeScale = TimeWarpingFactor;
        current = pauseMusic;
        PauseMusicSource.UnPause();
        current.TransitionTo(TransitionSeconds * TimeWarpingFactor);
        StartCoroutine(RepauseLater(TransitionSeconds * TimeWarpingFactor));
        StartCoroutine(PauseMusic(LevelMusicSource, current, TransitionSeconds * TimeWarpingFactor));
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        current = levelMusic;
        LevelMusicSource.UnPause();
        current.TransitionTo(TransitionSeconds);
        StartCoroutine(PauseMusic(PauseMusicSource, current, TransitionSeconds * TimeWarpingFactor));
    }

    public void AddPause()
    {
        Debug.Log(new System.Diagnostics.StackTrace());
        if (numLocks == 0)
        {
            Pause();
        }
        numLocks++;
    }

    public void RemovePause()
    {
        Debug.Log(new System.Diagnostics.StackTrace());
        if (numLocks == 0)
        {
            throw new InvalidOperationException();
        }
        numLocks--;
        if (numLocks == 0) UnPause();
    }

	// Use this for initialization
	void Start () {
        current = levelMusic;
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator RepauseLater(float time)
    {
        yield return new WaitForSeconds(time);

        if (Time.timeScale <= TimeWarpingFactor) Time.timeScale = 0;
    }

    IEnumerator PauseMusic(AudioSource toPause, AudioMixerSnapshot current, float time)
    {
        yield return new WaitForSeconds(time);

        if (this.current == current)
        {
            toPause.Pause();
        }
    }
}

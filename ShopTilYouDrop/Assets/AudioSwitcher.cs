using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioSwitcher : MonoBehaviour {

    public AudioMixerSnapshot levelMusic;
    public AudioSource LevelMusicSource;
    public AudioMixerSnapshot pauseMusic;
    public AudioSource PauseMusicSource;
    public float TransitionSeconds;
    public float TimeWarpingFactor;

    private AudioMixerSnapshot current;

	// Use this for initialization
	void Start () {
        current = levelMusic;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale <= TimeWarpingFactor)
        {
            if (current == levelMusic)
            {
                Time.timeScale = TimeWarpingFactor;
                current = pauseMusic;
                PauseMusicSource.UnPause();
                current.TransitionTo(TransitionSeconds * TimeWarpingFactor);
                StartCoroutine(RepauseLater(TransitionSeconds * TimeWarpingFactor));
                StartCoroutine(PauseMusic(LevelMusicSource, current, TransitionSeconds * TimeWarpingFactor));
            }
        }
        else
        {
            if (current == pauseMusic)
            {
                current = levelMusic;
                LevelMusicSource.UnPause();
                current.TransitionTo(TransitionSeconds);
                StartCoroutine(PauseMusic(PauseMusicSource, current, TransitionSeconds * TimeWarpingFactor));
            }
        }
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

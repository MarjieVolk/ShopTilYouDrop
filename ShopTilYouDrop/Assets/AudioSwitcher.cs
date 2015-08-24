using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioSwitcher : MonoBehaviour {

    public AudioMixerSnapshot levelMusic;
    public AudioMixerSnapshot pauseMusic;
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
                current.TransitionTo(TransitionSeconds * TimeWarpingFactor);
                StartCoroutine(RepauseLater(TransitionSeconds * TimeWarpingFactor));
            }
        }
        else
        {
            if (current == pauseMusic)
            {
                current = levelMusic;
                current.TransitionTo(TransitionSeconds);
            }
        }
	}

    IEnumerator RepauseLater(float time)
    {
        yield return new WaitForSeconds(time);

        if (Time.timeScale <= TimeWarpingFactor) Time.timeScale = 0;
    }
}

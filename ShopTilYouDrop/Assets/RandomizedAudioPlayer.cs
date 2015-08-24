using UnityEngine;
using System.Collections;

public class RandomizedAudioPlayer : MonoBehaviour {

    public const float lowPitchRange = 0.8f;
    public const float highPitchRange = 1.2f;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        source = gameObject.AddComponent<AudioSource>();
	}

    public void playSound(AudioClip[] clips) {
        if (clips.Length > 0) {
            int randomIndex = Random.Range(0, clips.Length - 1);
            playSound(clips[randomIndex]);
        }
    }

    public void playSound(AudioClip clip) {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        source.pitch = randomPitch;
        source.clip = clip;
        source.Play();
    }
}

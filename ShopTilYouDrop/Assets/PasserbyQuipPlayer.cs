using UnityEngine;
using System.Collections;

public class PasserbyQuipPlayer : MonoBehaviour {

    private static PasserbyQuipPlayer INSTANCE;

    public static PasserbyQuipPlayer instance() {
        return INSTANCE;
    }

    public AudioClip[] quips;

    private RandomizedAudioPlayer audioPlayer;

	void Start () {
        INSTANCE = this;
        audioPlayer = gameObject.AddComponent<RandomizedAudioPlayer>();
	}

    public void playQuip() {
        audioPlayer.playSound(quips);
    }
}

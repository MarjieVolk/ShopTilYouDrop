using UnityEngine;
using System.Collections;

public class PlayerSoundEffectPlayer : MonoBehaviour {

    private static PlayerSoundEffectPlayer INSTANCE;

    public static PlayerSoundEffectPlayer instance() {
        return INSTANCE;
    }

    private RandomizedAudioPlayer player;

	// Use this for initialization
	void Start () {
        INSTANCE = this;
        player = gameObject.AddComponent<RandomizedAudioPlayer>();
	}

    public void playSound(Aspects.Secondary type) {
        AudioClip[] sounds = PlayerSoundEffects.instance().getClips(type);
        player.playSound(sounds);
    }
}

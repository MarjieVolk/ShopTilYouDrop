using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerSoundEffects {

    private static PlayerSoundEffects INSTANCE = new PlayerSoundEffects();

    public static PlayerSoundEffects instance() {
        return INSTANCE;
    }

    private Dictionary<Aspects.Secondary, AudioClip[]> clips;

    PlayerSoundEffects() {
        clips = new Dictionary<Aspects.Secondary, AudioClip[]>();

        foreach (PlayerSoundEffectDescriptor descriptor in GameObject.FindObjectsOfType<PlayerSoundEffectDescriptor>()) {
            clips.Add(descriptor.type, descriptor.sounds);
        }
    }

    public AudioClip[] getClips(Aspects.Secondary type) {
        if (clips.ContainsKey(type)) {
            return clips[type];
        } else {
            return new AudioClip[0];
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class Aspects {

    public enum Primary {
        DAIRY,
        MEAT,
        GRAIN,
        PLANT,
        UNKNOWN
    }

    public enum Secondary {
        VOID,
        ANGELIC,
        FIRE,
        WATER,
        SLIME,
        DECAY,
        BLOOD,
        NONE,
        UNKNOWN
    }

    private static Aspects INSTANCE = new Aspects();

    public static Aspects instance() {
        return INSTANCE;
    }

    private Dictionary<Primary, AspectData> primaries;
    private Dictionary<Secondary, AspectData> secondaries;

    Aspects() {
        primaries = new Dictionary<Primary, AspectData>();
        foreach (AspectSpriteDescriptorPrimary descriptor in GameObject.FindObjectsOfType<AspectSpriteDescriptorPrimary>()) {
            primaries.Add(descriptor.type, new AspectData(descriptor.normal, descriptor.greyed, descriptor.disabled));
        }

        secondaries = new Dictionary<Secondary, AspectData>();
        foreach (AspectSpriteDescriptorSecondary descriptor in GameObject.FindObjectsOfType<AspectSpriteDescriptorSecondary>()) {
            secondaries.Add(descriptor.type, new AspectData(descriptor.normal, descriptor.greyed, descriptor.disabled));
        }
    }

    public Sprite getNormalSprite(Primary type) {
        return primaries[type].normal;
    }

    public Sprite getNormalSprite(Secondary type) {
        return secondaries[type].normal;
    }

    public Sprite getGreyedSprite(Primary type) {
        return primaries[type].greyed;
    }

    public Sprite getGreyedSprite(Secondary type) {
        return secondaries[type].greyed;
    }

    public Sprite getDisabledSprite(Primary type) {
        return primaries[type].disabled;
    }

    public Sprite getDisabledSprite(Secondary type) {
        return secondaries[type].disabled;
    }

    private class AspectData {
        public readonly Sprite normal, greyed, disabled;

        public AspectData(Sprite normal, Sprite greyed, Sprite disabled) {
            this.normal = normal;
            this.greyed = greyed;
            this.disabled = disabled;
        }
    }
}

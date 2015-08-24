using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PotionDescriptor : MonoBehaviour {
    public Aspects.Primary[] PrimaryAspects = new Aspects.Primary[3];
    public PotionSlot Slot;
    public Effect EffectNone;
    public Effect EffectVoid;
    public Effect EffectSeductive;
    public Effect EffectFire;
    public Effect EffectWater;
    public Effect EffectSlime;
    public Effect EffectDecay;
    public Effect EffectBeastly;

	// Use this for initialization
	void Start () {
        foreach (Aspects.Secondary aspect in Enum.GetValues(typeof(Aspects.Secondary)))
        {
            Effect effect = getEffectForAspect(aspect);
            Potions.instance().add(
                PrimaryAspects[0],
                PrimaryAspects[1],
                PrimaryAspects[2],
                new List<Aspects.Secondary>() { aspect },
                Slot,
                aspect,
                effect);
        }

        Destroy(this);
	}

    private Effect getEffectForAspect(Aspects.Secondary aspect)
    {
        switch (aspect)
        {
            case Aspects.Secondary.NONE:
                return EffectNone;
            case Aspects.Secondary.VOID:
                return EffectVoid;
            case Aspects.Secondary.SEDUCTIVE:
                return EffectSeductive;
            case Aspects.Secondary.FIRE:
                return EffectFire;
            case Aspects.Secondary.WATER:
                return EffectWater;
            case Aspects.Secondary.SLIME:
                return EffectSlime;
            case Aspects.Secondary.DECAY:
                return EffectDecay;
            case Aspects.Secondary.BEAST:
                return EffectBeastly;
            case Aspects.Secondary.UNKNOWN:
                return null;
            default:
                throw new ArgumentException("Unrecognized Secondary aspect enum value: " + aspect);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

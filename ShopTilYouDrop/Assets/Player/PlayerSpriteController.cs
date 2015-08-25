using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerSpriteController : MonoBehaviour {

    private IDictionary<PotionSlot, Effect> _potionEffects;

    public PlayerSpriteController()
    {
        _potionEffects = new Dictionary<PotionSlot, Effect>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setEffect(PotionSlot part, Effect effect)
    {
        if (_potionEffects.ContainsKey(part)) {
            Effect currentEffect = _potionEffects[part];
            if (currentEffect != null) {
                currentEffect.UnTrigger();
            }
        }

        _potionEffects[part] = effect;
        if (effect != null)
        {
            effect.Trigger();
        }
    }

    public void setBodyParts(BodyPart[] parts, Aspects.Secondary type)
    {
        foreach (BodyPart part in parts)
        {
            setBodyPart(part, type);
        }
    }

    public void setBodyPart(BodyPart part, Aspects.Secondary type) {
        Transform partTransform = bodyPartToTransform(part);

        partTransform.GetComponent<SpriteRenderer>().sprite = PlayerSprites.instance().getSprite(type, part);
    }

    public Aspects.Secondary GetAspectForBodyPart(BodyPart part)
    {
        Transform partTransform = bodyPartToTransform(part);

        return PlayerSprites.instance().getAspect(part, partTransform.GetComponent<SpriteRenderer>().sprite);
    }

    private Transform bodyPartToTransform(BodyPart part)
    {
        Transform partTransform;
        switch (part)
        {
            case BodyPart.HEAD:
                partTransform = transform.Find("Head");
                break;
            case BodyPart.FACE:
                Debug.Log("Setting face");
                partTransform = transform.Find("Face");
                break;
            case BodyPart.LEFT_ARM:
                partTransform = transform.Find("Arms_l");
                break;
            case BodyPart.LEFT_LEG:
                partTransform = transform.Find("Legs_l");
                break;
            case BodyPart.RIGHT_ARM:
                partTransform = transform.Find("Arms_r");
                break;
            case BodyPart.RIGHT_LEG:
                partTransform = transform.Find("Legs_r");
                break;
            case BodyPart.TAIL:
                partTransform = transform.Find("Tail");
                break;
            case BodyPart.TORSO:
                partTransform = transform.Find("Torso");
                break;
            case BodyPart.WINGS:
                partTransform = transform.Find("Wings");
                break;
            default:
                throw new Exception("Unexpected enum value " + part);
        };
        return partTransform;
    }

    public void setHeadWizard() {
        setBodyPart(BodyPart.HEAD, Aspects.Secondary.NONE);
    }

    public void setHeadSlime() {
        setBodyPart(BodyPart.HEAD, Aspects.Secondary.SLIME);
    }

    public void setHeadWater() {
        setBodyPart(BodyPart.HEAD, Aspects.Secondary.WATER);
    }

    public void setHeadWolf() {
        setBodyPart(BodyPart.HEAD, Aspects.Secondary.BEAST);
    }

    public void setHeadZombie() {
        setBodyPart(BodyPart.HEAD, Aspects.Secondary.DECAY);
    }

    public void setFaceWizard() {
        setBodyPart(BodyPart.FACE, Aspects.Secondary.NONE);
    }

    public void setFaceSlime() {
        setBodyPart(BodyPart.FACE, Aspects.Secondary.SLIME);
    }

    public void setFaceWater() {
        setBodyPart(BodyPart.FACE, Aspects.Secondary.WATER);
    }

    public void setFaceWolf() {
        setBodyPart(BodyPart.FACE, Aspects.Secondary.BEAST);
    }

    public void setFaceZombie() {
        setBodyPart(BodyPart.FACE, Aspects.Secondary.DECAY);
    }

    public void setTorsoWizard() {
        setBodyPart(BodyPart.TORSO, Aspects.Secondary.NONE);
    }

    public void setTorsoSlime() {
        setBodyPart(BodyPart.TORSO, Aspects.Secondary.SLIME);
    }

    public void setTorsoWater() {
        setBodyPart(BodyPart.TORSO, Aspects.Secondary.WATER);
    }

    public void setTorsoWolf() {
        setBodyPart(BodyPart.TORSO, Aspects.Secondary.BEAST);
    }

    public void setTorsoZombie() {
        setBodyPart(BodyPart.TORSO, Aspects.Secondary.DECAY);
    }

    public void setArmsWizard() {
        setBodyPart(BodyPart.LEFT_ARM, Aspects.Secondary.NONE);
        setBodyPart(BodyPart.RIGHT_ARM, Aspects.Secondary.NONE);
    }

    public void setArmsSlime() {
        setBodyPart(BodyPart.LEFT_ARM, Aspects.Secondary.SLIME);
        setBodyPart(BodyPart.RIGHT_ARM, Aspects.Secondary.SLIME);
    }

    public void setArmsWater() {
        setBodyPart(BodyPart.LEFT_ARM, Aspects.Secondary.WATER);
        setBodyPart(BodyPart.RIGHT_ARM, Aspects.Secondary.WATER);
    }

    public void setArmsWolf() {
        setBodyPart(BodyPart.LEFT_ARM, Aspects.Secondary.BEAST);
        setBodyPart(BodyPart.RIGHT_ARM, Aspects.Secondary.BEAST);
    }

    public void setArmsZombie() {
        setBodyPart(BodyPart.LEFT_ARM, Aspects.Secondary.DECAY);
        setBodyPart(BodyPart.RIGHT_ARM, Aspects.Secondary.DECAY);
    }

    public void setLegsWizard() {
        setBodyPart(BodyPart.LEFT_LEG, Aspects.Secondary.NONE);
        setBodyPart(BodyPart.RIGHT_LEG, Aspects.Secondary.NONE);
    }

    public void setLegsSlime() {
        setBodyPart(BodyPart.LEFT_LEG, Aspects.Secondary.SLIME);
        setBodyPart(BodyPart.RIGHT_LEG, Aspects.Secondary.SLIME);
    }

    public void setLegsWater() {
        setBodyPart(BodyPart.LEFT_LEG, Aspects.Secondary.WATER);
        setBodyPart(BodyPart.RIGHT_LEG, Aspects.Secondary.WATER);
    }

    public void setLegsWolf() {
        setBodyPart(BodyPart.LEFT_LEG, Aspects.Secondary.BEAST);
        setBodyPart(BodyPart.RIGHT_LEG, Aspects.Secondary.BEAST);
    }

    public void setLegsZombie() {
        setBodyPart(BodyPart.LEFT_LEG, Aspects.Secondary.DECAY);
        setBodyPart(BodyPart.RIGHT_LEG, Aspects.Secondary.DECAY);
    }

    public void setTailWizard() {
        setBodyPart(BodyPart.TAIL, Aspects.Secondary.NONE);
    }

    public void setTailSlime() {
        setBodyPart(BodyPart.TAIL, Aspects.Secondary.SLIME);
    }

    public void setWingsWizard() {
        setBodyPart(BodyPart.WINGS, Aspects.Secondary.NONE);
    }

    public void setWingsSlime() {
        setBodyPart(BodyPart.WINGS, Aspects.Secondary.SLIME);
    }
}

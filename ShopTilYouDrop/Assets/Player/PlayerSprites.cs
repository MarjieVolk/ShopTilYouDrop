using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerSprites {

    private static PlayerSprites INSTANCE = new PlayerSprites();

    Dictionary<Aspects.Secondary, Dictionary<BodyPart, Sprite>> sprites = new Dictionary<Aspects.Secondary, Dictionary<BodyPart, Sprite>>();

    public static PlayerSprites instance() {
        return INSTANCE;
    }

    PlayerSprites() {
        foreach (PlayerSpriteDescriptor spriteDescriptor in GameObject.FindObjectsOfType<PlayerSpriteDescriptor>()) {
            Dictionary<BodyPart, Sprite> typeSprites = new Dictionary<BodyPart, Sprite>();
            typeSprites.Add(BodyPart.FACE, spriteDescriptor.face);
            typeSprites.Add(BodyPart.HEAD, spriteDescriptor.head);
            typeSprites.Add(BodyPart.LEFT_ARM, spriteDescriptor.leftArm);
            typeSprites.Add(BodyPart.LEFT_LEG, spriteDescriptor.leftLeg);
            typeSprites.Add(BodyPart.RIGHT_ARM, spriteDescriptor.rightArm);
            typeSprites.Add(BodyPart.RIGHT_LEG, spriteDescriptor.rightLeg);
            typeSprites.Add(BodyPart.TAIL, spriteDescriptor.tail);
            typeSprites.Add(BodyPart.TORSO, spriteDescriptor.torso);
            typeSprites.Add(BodyPart.WINGS, spriteDescriptor.wings);

            sprites.Add(spriteDescriptor.type, typeSprites);
        }
    }

    public Sprite getSprite(Aspects.Secondary type, BodyPart bodyPart) {
        return sprites[type][bodyPart];
    }

}

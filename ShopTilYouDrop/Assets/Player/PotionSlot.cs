using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum PotionSlot
{
    HEAD, FACE, TORSO, ARMS, LEGS, WINGS, TAIL, NONE
}

public static class PotionSlotExtensions
{
    public static BodyPart[] ToBodyParts(this PotionSlot potionSlot)
    {
        switch (potionSlot)
        {
            case PotionSlot.HEAD:
                return new BodyPart[] { BodyPart.HEAD };
            case PotionSlot.FACE:
                return new BodyPart[] { BodyPart.FACE };
            case PotionSlot.TORSO:
                return new BodyPart[] { BodyPart.TORSO };
            case PotionSlot.ARMS:
                return new BodyPart[] { BodyPart.LEFT_ARM, BodyPart.RIGHT_ARM };
            case PotionSlot.LEGS:
                return new BodyPart[] { BodyPart.LEFT_LEG, BodyPart.RIGHT_LEG };
            case PotionSlot.WINGS:
                return new BodyPart[] { BodyPart.WINGS };
            case PotionSlot.TAIL:
                return new BodyPart[] { BodyPart.TAIL };
            case PotionSlot.NONE:
                return new BodyPart[] { };
            default:
                throw new ArgumentException("Unrecognized PotionSlot enum value: " + potionSlot);
        }
    }
}
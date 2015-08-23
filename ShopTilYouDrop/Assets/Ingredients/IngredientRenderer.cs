using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientRenderer {

    private static IngredientRenderer INSTANCE = new IngredientRenderer();

    public static IngredientRenderer instance() {
        return INSTANCE;
    }

    private Dictionary<IngredientType, Sprite> sprites = new Dictionary<IngredientType, Sprite>();

    IngredientRenderer() {
        foreach (IngredientSpriteDescriptor spriteDescriptor in GameObject.FindObjectsOfType<IngredientSpriteDescriptor>()) {
            sprites.Add(spriteDescriptor.type, spriteDescriptor.sprite);
        }
    }

    public Sprite getSprite(IngredientType type) {
        return sprites[type];
    }
}

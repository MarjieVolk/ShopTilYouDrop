﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ingredients {

    public static const Ingredients INSTANCE = new Ingredients();

    private Dictionary<IngredientType, IngredientData> ingredients;

    Ingredients() {
        ingredients = new Dictionary<IngredientType, IngredientData>();
        add(IngredientType.CHEESE, Aspects.Primary.DAIRY, Aspects.Secondary.NONE);
    }

    public IngredientData getIngredient(IngredientType type) {
        return ingredients[type];
    }

    private void add(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        ingredients.Add(type, new IngredientData(type, primary, secondary));
    }
}

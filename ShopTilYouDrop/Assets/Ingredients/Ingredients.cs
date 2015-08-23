using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Ingredients {

    private static Ingredients INSTANCE = new Ingredients();

    public static Ingredients instance() {
        return INSTANCE;
    }

    private Dictionary<IngredientType, IngredientData> ingredients;

    Ingredients() {
        ingredients = new Dictionary<IngredientType, IngredientData>();
        add(IngredientType.CHEESE, Aspects.Primary.DAIRY, Aspects.Secondary.FIRE);
    }

    public IngredientData getIngredient(IngredientType type) {
        return ingredients[type];
    }

    private void add(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        ingredients.Add(type, new IngredientData(type, primary, secondary));
    }
}

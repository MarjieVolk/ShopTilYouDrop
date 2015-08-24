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

        foreach (IngredientType type in Enum.GetValues(typeof(IngredientType))) {
            add(type, type.GetPrimaryAspect(), type.GetSecondaryAspect());
        }
    }

    public IngredientData getIngredient(IngredientType type) {
        return ingredients[type];
    }

    private void add(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        ingredients.Add(type, new IngredientData(type, primary, secondary));
    }
}

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
    private HashSet<IngredientType> seenIngredients;

    Ingredients() {
        ingredients = new Dictionary<IngredientType, IngredientData>();
        seenIngredients = new HashSet<IngredientType>();

        foreach (IngredientType type in Enum.GetValues(typeof(IngredientType))) {
            add(type, type.GetPrimaryAspect(), type.GetSecondaryAspect());
        }
    }

    public void addSeenIngredient(IngredientType type) {
        seenIngredients.Add(type);
    }

    public HashSet<IngredientType> getSeenIngredients() {
        return new HashSet<IngredientType>(seenIngredients);
    }

    public IngredientData getIngredient(IngredientType type) {
        return ingredients[type];
    }

    private void add(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        ingredients.Add(type, new IngredientData(type, primary, secondary));
    }
}

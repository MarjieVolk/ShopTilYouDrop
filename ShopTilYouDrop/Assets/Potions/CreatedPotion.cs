using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CreatedPotion {

    private Potion potion;
    private List<IngredientType> ingredients;

    public CreatedPotion(Potion potion, IngredientType ingredient1, IngredientType ingredient2, IngredientType ingredient3) {
        this.potion = potion;
        ingredients = new List<IngredientType>();
        ingredients.Add(ingredient1);
        ingredients.Add(ingredient2);
        ingredients.Add(ingredient3);
    }

    public List<IngredientType> getIngredients() {
        return new List<IngredientType>(ingredients);
    }

    public Potion getPotion() {
        return potion;
    }
}

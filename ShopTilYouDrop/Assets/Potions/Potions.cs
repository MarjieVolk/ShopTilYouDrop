using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Potions {

    private static Potions INSTANCE = new Potions();

    public static Potions instance() {
        return INSTANCE;
    }

    private Dictionary<HashSet<Aspects.Primary>, List<Potion>> potions;
    private List<CreatedPotion> createdPotions;

    Potions() {
        potions = new Dictionary<HashSet<Aspects.Primary>, List<Potion>>();
        createdPotions = new List<CreatedPotion>();
    }

    private void add() {

    }

    public Potion createPotion(IngredientType ingredient1, IngredientType ingredient2, IngredientType ingredient3) {
        IngredientData data1 = Ingredients.instance().getIngredient(ingredient1);
        IngredientData data2 = Ingredients.instance().getIngredient(ingredient2);
        IngredientData data3 = Ingredients.instance().getIngredient(ingredient3);

        List<Aspects.Secondary> secondaries = new List<Aspects.Secondary>();
        secondaries.Add(data1.secondary);
        secondaries.Add(data1.secondary);
        secondaries.Add(data1.secondary);

        Potion createdPotion = getBestMatch(data1.primary, data2.primary, data3.primary, secondaries);
        logPotionCreation(createdPotion, ingredient1, ingredient2, ingredient3);
        return createdPotion;
    }

    public List<CreatedPotion> getLoggedPotions() {
        return new List<CreatedPotion>(createdPotions);
    }

    // Note (marjie): I'm choosing to only allow removing via index, because if we're displaying this in the UI as an ordered list
    // and the same potion can be created with the same ingredients multiple times, we always want to remove the exact one clicked on
    public void removeLoggedPotion(int index) {
        createdPotions.RemoveAt(index);
    }

    private void logPotionCreation(Potion potionMade, IngredientType ingredient1, IngredientType ingredient2, IngredientType ingredient3) {
        createdPotions.Add(new CreatedPotion(potionMade, ingredient1, ingredient2, ingredient3));
    }

    private Potion getBestMatch(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3, List<Aspects.Secondary> secondaries) {
        HashSet<Aspects.Primary> primaries = new HashSet<Aspects.Primary>();
        List<Potion> primaryMatches = potions[primaries];

        Potion bestMatch = null;
        foreach (Potion primaryMatch in primaryMatches) {
            bool match = true;
            foreach (Aspects.Secondary secondaryRequirement in primaryMatch.getSecondaries()) {
                if (!secondaries.Contains(secondaryRequirement)) {
                    match = false;
                    break;
                }
            }

            if (match && (bestMatch == null || bestMatch.getSecondaries().Count() < primaryMatch.getSecondaries().Count())) {
                bestMatch = primaryMatch;
            }
        }

        return bestMatch;
    }
}

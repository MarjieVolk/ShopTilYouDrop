using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Potions {

    private static Potions INSTANCE = new Potions();

    public static Potions instance() {
        return INSTANCE;
    }

    private Dictionary<MultiSet<Aspects.Primary>, List<Potion>> potions;
    private List<CreatedPotion> createdPotions;
    private Potion defaultPotion;

    Potions() {
        potions = new Dictionary<MultiSet<Aspects.Primary>, List<Potion>>(new MultiSetComparer());
        createdPotions = new List<CreatedPotion>();

        defaultPotion = new Potion(Aspects.Primary.UNKNOWN, Aspects.Primary.UNKNOWN, Aspects.Primary.UNKNOWN, new List<Aspects.Secondary>(), PotionSlot.NONE, Aspects.Secondary.NONE, null);
    }

    public void add(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3, List<Aspects.Secondary> secondaries, PotionSlot slot, Aspects.Secondary type, Effect effect) {
        Potion potion = new Potion(primary1, primary2, primary3, secondaries, slot, type, effect);
        MultiSet<Aspects.Primary> primaries = new MultiSet<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);

        if (!potions.ContainsKey(primaries)) {
            potions.Add(primaries, new List<Potion>());
        }

        potions[primaries].Add(potion);
    }

    public Potion createPotion(IngredientType ingredient1, IngredientType ingredient2, IngredientType ingredient3) {
        Debug.Log("Creating potion with " + ingredient1 + ", " + ingredient2 + ", " + ingredient3);
        IngredientData data1 = Ingredients.instance().getIngredient(ingredient1);
        IngredientData data2 = Ingredients.instance().getIngredient(ingredient2);
        IngredientData data3 = Ingredients.instance().getIngredient(ingredient3);

        MultiSet<Aspects.Secondary> secondaries = new MultiSet<Aspects.Secondary>();
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

    public void removeLoggedPotion(CreatedPotion potion) {
        createdPotions.Remove(potion);
    }

    private void logPotionCreation(Potion potionMade, IngredientType ingredient1, IngredientType ingredient2, IngredientType ingredient3) {
        createdPotions.Add(new CreatedPotion(potionMade, ingredient1, ingredient2, ingredient3));
    }

    private Potion getBestMatch(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3, MultiSet<Aspects.Secondary> secondaries) {
        MultiSet<Aspects.Primary> primaries = new MultiSet<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);

        if (!potions.ContainsKey(primaries)) {
            Debug.Log("No key for " + primaries.ToString() + " count=" + primaries.Count);
            return defaultPotion;
        }

        List<Potion> primaryMatches = potions[primaries];

        Potion bestMatch = null;
        foreach (Potion primaryMatch in primaryMatches) {
            bool match = (primaryMatch.getSecondaries().Except(secondaries).Count == 0);

            if (match && (bestMatch == null || bestMatch.getSecondaries().Count() < primaryMatch.getSecondaries().Count())) {
                bestMatch = primaryMatch;
            }
        }

        return bestMatch;
    }

    private class MultiSetComparer : IEqualityComparer<MultiSet<Aspects.Primary>> {

        public bool Equals(MultiSet<Aspects.Primary> x, MultiSet<Aspects.Primary> y)
        {
            return x.Except(y).Count == 0 && y.Except(x).Count == 0;
        }

        public int GetHashCode(MultiSet<Aspects.Primary> obj)
        {
            int val = 0;
            int multiplier = 1;
            foreach (Aspects.Primary aspect in obj)
            {
                val += ((int)aspect) * multiplier;
                multiplier *= 10;
            }
            return val;
        }
    }
}

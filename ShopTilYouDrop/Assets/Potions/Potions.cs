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

    private Dictionary<List<Aspects.Primary>, List<Potion>> potions;
    private List<CreatedPotion> createdPotions;
    private Potion defaultPotion;

    Potions() {
        potions = new Dictionary<List<Aspects.Primary>, List<Potion>>(new ListComparer());
        createdPotions = new List<CreatedPotion>();

        defaultPotion = new Potion(Aspects.Primary.UNKNOWN, Aspects.Primary.UNKNOWN, Aspects.Primary.UNKNOWN, new List<Aspects.Secondary>(), PotionSlot.NONE, Aspects.Secondary.NONE, null);
    }

    public void add(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3, List<Aspects.Secondary> secondaries, PotionSlot slot, Aspects.Secondary type, Effect effect) {
        Potion potion = new Potion(primary1, primary2, primary3, secondaries, slot, type, effect);
        List<Aspects.Primary> primaries = new List<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);
        primaries.Sort();

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

        List<Aspects.Secondary> secondaries = new List<Aspects.Secondary>();
        secondaries.Add(data1.secondary);
        secondaries.Add(data1.secondary);
        secondaries.Add(data1.secondary);
        secondaries.Sort();

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

    private Potion getBestMatch(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3, List<Aspects.Secondary> secondaries) {
        List<Aspects.Primary> primaries = new List<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);
        primaries.Sort();

        if (!potions.ContainsKey(primaries)) {
            foreach (List<Aspects.Primary> list in potions.Keys) {
                Debug.Log("No key for (" + list[0] + ", " + list[1] + ", " + list[2] + ") count=" + list.Count);
            }
            return defaultPotion;
        }

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

    private class ListComparer : IEqualityComparer<List<Aspects.Primary>> {

        public bool Equals(List<Aspects.Primary> x, List<Aspects.Primary> y) {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<Aspects.Primary> obj) {
            int val = 0;
            int multiplier = 1;
            foreach (Aspects.Primary aspect in obj) {
                val += ((int) aspect) * multiplier;
                multiplier *= 10;
            }
            return val;
        }
    }
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour {

    public float consumeDelaySeconds;

    private Dictionary<Ingredient, float> enterTimes;
    private List<IngredientType> added;

	void Start () {
        enterTimes = new Dictionary<Ingredient, float>();
	}
	
	void Update () {
        List<Ingredient> toConsume = new List<Ingredient>();
	    foreach (Ingredient ingredient in enterTimes.Keys) {
            if (Time.time - enterTimes[ingredient] >= consumeDelaySeconds) {
                toConsume.Add(ingredient);
            }
        }

        foreach (Ingredient ingredient in toConsume) {
            enterTimes.Remove(ingredient);
            consumeIngredient(ingredient);
        }
	}

    void OnTriggerEnter2D(Collider2D collision) {
        Ingredient collidingIngredient = collision.gameObject.GetComponent<Ingredient>();
        if (collidingIngredient != null) {
            enterTimes.Add(collidingIngredient, Time.time);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        Ingredient collidingIngredient = collision.gameObject.GetComponent<Ingredient>();
        if (collidingIngredient != null) {
            enterTimes.Remove(collidingIngredient);
        }
    }

    private void consumeIngredient(Ingredient ingredient) {
        added.Add(ingredient.type);
        Destroy(ingredient.gameObject);

        if (added.Count >= 3) {
            createPotion(added[0], added[1], added[2]);
            added.Remove(0);
            added.Remove(0);
            added.Remove(0);
        }
    }

    private void createPotion(IngredientType one, IngredientType two, IngredientType three) {
        Potion potion = Potions.instance().createPotion(one, two, three);
        // TODO: activate potion effect
    }
}

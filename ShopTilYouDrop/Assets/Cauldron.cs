using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour {

    public float consumeDelaySeconds;
    public AudioClip consumeIngredientSound;

    private Dictionary<Ingredient, float> enterTimes;
    private List<IngredientType> added;
    private CauldronHUDController HUD;

    private RandomizedAudioPlayer audio;

    void Start() {
        audio = gameObject.AddComponent<RandomizedAudioPlayer>();

        enterTimes = new Dictionary<Ingredient, float>();
        added = new List<IngredientType>();
        HUD = GameObject.FindObjectOfType<CauldronHUDController>();
    }

    void Update() {
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
        audio.playSound(consumeIngredientSound);

        added.Add(ingredient.type);
        HUD.notifyIngredientAdded(new List<IngredientType>(added));
        Destroy(ingredient.gameObject);

        if (added.Count >= 3) {
            createPotion(added[0], added[1], added[2]);
            added.RemoveAt(0);
            added.RemoveAt(0);
            added.RemoveAt(0);
            HUD.notifyCauldronCleared();
        }
    }

    private void createPotion(IngredientType one, IngredientType two, IngredientType three) {
        Potion potion = Potions.instance().createPotion(one, two, three);
        potion.TriggerEffect();
    }
}

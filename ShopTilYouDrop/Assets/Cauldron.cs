using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cauldron : MonoBehaviour {

    private Dictionary<Ingredient, float> enterTimes;

    public float consumeDelaySeconds;

	void Start () {
        enterTimes = new Dictionary<Ingredient, float>();
	}
	
	void Update () {
	    foreach (Ingredient ingredient in enterTimes.Keys) {
            if (Time.time - enterTimes[ingredient] >= consumeDelaySeconds) {
                enterTimes.Remove(ingredient);
                consumeIngredient(ingredient);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Object entered collision: " + collision.gameObject);
        Ingredient collidingIngredient = collision.gameObject.GetComponent<Ingredient>();
        if (collidingIngredient != null) {
            enterTimes.Add(collidingIngredient, Time.time);
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("Object exited collision: " + collision.gameObject);
        Ingredient collidingIngredient = collision.gameObject.GetComponent<Ingredient>();
        if (collidingIngredient != null) {
            enterTimes.Remove(collidingIngredient);
        }
    }

    private void consumeIngredient(Ingredient ingredient) {
        //TODO: add ingredient to potion
        Destroy(ingredient.gameObject);
    }
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DenseIngredientsGenerator : MonoBehaviour, IIngredientGenerator {

    public IngredientType[] ingredients;
    public float SwitchProbability;

    private ShelfInventory shelf;
    private IngredientType currentIngredient;

	// Use this for initialization
	void Start () {
        shelf = FindObjectOfType<ShelfInventory>();
        shelf.registerGenerator(this);
        currentIngredient = selectIngredient();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IngredientType? TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex)
    {
        if (shelfIndex == 0 && Random.value < SwitchProbability)
        {
            currentIngredient = selectIngredient();
        }

        if (shelfSpace[shelfIndex] > 0)
        {
            return currentIngredient;
        }
        return null;
    }

    private IngredientType selectIngredient()
    {
        return ingredients[Random.Range(0, ingredients.Length)];
    }

    void OnDestroy() {
        shelf.unregisterGenerator(this);
    }
}

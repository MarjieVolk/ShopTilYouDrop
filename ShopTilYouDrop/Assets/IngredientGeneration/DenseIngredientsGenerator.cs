using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DenseIngredientsGenerator : MonoBehaviour, IIngredientGenerator {

    public GameObject[] ingredients;
    public float SwitchProbability;

    private ShelfInventory shelf;
    private GameObject currentIngredient;

	// Use this for initialization
	void Start () {
        shelf = FindObjectOfType<ShelfInventory>();
        shelf.registerGenerator(this);
        currentIngredient = selectIngredient();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex)
    {
        if (shelfIndex == 0 && Random.value < SwitchProbability)
        {
            currentIngredient = selectIngredient();
        }
        float requiredSpace = currentIngredient.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        if (shelfSpace[shelfIndex] > 0)
        {
            return Instantiate(currentIngredient);
        }
        return null;
    }

    private GameObject selectIngredient()
    {
        return ingredients[Random.Range(0, ingredients.Length)];
    }
}

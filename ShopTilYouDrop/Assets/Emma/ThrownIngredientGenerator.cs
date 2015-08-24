using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrownIngredientGenerator : MonoBehaviour {

	// For thrown objects, this value should be pretty high.
	// The more ingredients thrown, the higher this value should be for each one.
	public float MeanTimeBetweenAppearances;
	public IngredientType ingredient;
	
	private static System.Random random = new System.Random();
	
	private PasserbyInventory[] inventories;
	private float nextSpawnTime;
	
	void Start () {
		random = new System.Random();
		inventories = FindObjectsOfType<PasserbyInventory>();

        nextSpawnTime = SampleDelay();
	}

    void Update() {
        if (Time.time > nextSpawnTime) {
            nextSpawnTime += SampleDelay();
            inventories[random.Next(inventories.Length)].throwIngredient(ingredient);
            PasserbyQuipPlayer.instance().playQuip();
        }
    }
	
	private float SampleDelay()
	{
        return (float) random.NextDouble() * MeanTimeBetweenAppearances * 2;
        //return -1 * (float) System.Math.Log(1 - random.NextDouble(), System.Math.E) * MeanTimeBetweenAppearances;
	}
}

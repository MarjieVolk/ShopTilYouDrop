using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrownIngredientGenerator : MonoBehaviour, IIngredientGenerator {

	// For thrown objects, this value should be pretty high.
	// The more ingredients thrown, the higher this value should be for each one.
	public float MeanTimeBetweenAppearances;
	public IngredientType ingredient;
	
	private static System.Random random = new System.Random();
	
	private PasserbyInventory shelf;
	private float nextSpawnTime;
	
	void Start () {
		random = new System.Random();
		shelf = FindObjectOfType<PasserbyInventory>();
		shelf.registerGenerator(this);
		
		nextSpawnTime = MeanTimeBetweenAppearances;
	}
	
	public IngredientType? TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex)
	{
		if (Time.time > nextSpawnTime)
		{
			nextSpawnTime += SampleDelay();
			return ingredient;
		}
		
		return null;
	}
	
	private float SampleDelay()
	{
		return -1 * (float) System.Math.Log(1 - random.NextDouble(), System.Math.E) * MeanTimeBetweenAppearances;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrownIngredientGenerator : MonoBehaviour, IIngredientGenerator {
	
	public float MeanTimeBetweenAppearances;
	public IngredientType ingredient;
	
	private static System.Random random = new System.Random();
	
	private PasserbyInventory shelf;
	private float nextSpawnTime;
	
	// Use this for initialization
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
		// p = 1 - e^-lx
		// 1 - p = e^-lx
		// log_e(1 - p) = -lx
		// log_e(1-p) / -l = x
		// log_e(1-p) * -B
		
		return -1 * (float) System.Math.Log(1 - random.NextDouble(), System.Math.E) * MeanTimeBetweenAppearances;
	}
}

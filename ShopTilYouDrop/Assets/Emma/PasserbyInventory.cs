using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PasserbyInventory : MonoBehaviour {
	
	// Makes passerby throw things

	public Vector3 SpawnPosition;
	public Vector2 Speed;
	public float Gravity;
	public LayerMask SpawnedObjectsLayer;
	public GameObject IngredientUniversalPrefab;
	
	// The available strategies for ingredient placement
	private IList<IIngredientGenerator> generators;
	
	public PasserbyInventory()
	{
		generators = new List<IIngredientGenerator>();
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update()
	{
		Shuffle(generators);
		foreach (IIngredientGenerator generator in generators)
		{
			IngredientType? type = generator.TryPlaceIngredient(null, 0);
			if (type != null)
			{
				// TODO: Put a "scared bystander" sound effect here!!!

				GameObject instantiated = Instantiate(IngredientUniversalPrefab);
				instantiated.GetComponent<Ingredient>().type = (IngredientType)type;
				instantiated.GetComponent<Ingredient>().InitializeGameObject();

				instantiated.transform.position = SpawnPosition;
				instantiated.GetComponent<Rigidbody2D>().isKinematic = true;
				instantiated.AddComponent<ThrowingMovement>().Step = Speed;
				instantiated.GetComponent<ThrowingMovement>().Gravity = Gravity;
				instantiated.layer = SpawnedObjectsLayer.getFirstSet();
			}
		}
	}
	
	public void registerGenerator(IIngredientGenerator generator)
	{
		generators.Add(generator);
	}
	
	private static System.Random rng = new System.Random();
	
	// Fisher-Yates
	public static void Shuffle<T>(IList<T> list)
	{
		int i = 0;
		while (i < list.Count)
		{
			int target = rng.Next(i, list.Count);
			T temp = list[i];
			list[i] = list[target];
			list[target] = temp;
			i++;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PasserbyInventory : MonoBehaviour {
	
	// Makes passerby throw things

	public float maxSpawnPositionOffset;
	public Vector2 Speed;
	public GameObject IngredientUniversalPrefab;

	public void throwIngredient(IngredientType type) {
		GameObject instantiated = Instantiate(IngredientUniversalPrefab);
		instantiated.GetComponent<Ingredient>().type = (IngredientType)type;
		instantiated.GetComponent<Ingredient>().InitializeGameObject();

        Vector3 newPosition = transform.position;
        newPosition.x += (float) (rng.NextDouble() * maxSpawnPositionOffset * 2) - maxSpawnPositionOffset;
        newPosition.y += (float) (rng.NextDouble() * maxSpawnPositionOffset * 2) - maxSpawnPositionOffset;
		instantiated.transform.position = newPosition;
        instantiated.GetComponent<Rigidbody2D>().velocity = Speed;
	}
	
	private static System.Random rng = new System.Random();
}

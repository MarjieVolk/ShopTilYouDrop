using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SparseIngredientGenerator : MonoBehaviour, IIngredientGenerator {

    public float MeanTimeBetweenAppearances;
    public GameObject ingredient;

    private static System.Random random = new System.Random();

    private ShelfInventory shelf;
    private float[] _nextSpawnTime;

	// Use this for initialization
	void Start () {
        random = new System.Random();
        shelf = FindObjectOfType<ShelfInventory>();
        shelf.registerGenerator(this);

        _nextSpawnTime = new float[shelf.NumShelves];
	}

    public GameObject TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex)
    {
        if (shelfSpace[shelfIndex] > 0 && Time.time > _nextSpawnTime[shelfIndex])
        {
            _nextSpawnTime[shelfIndex] += SampleDelay();
            return Instantiate(ingredient);
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

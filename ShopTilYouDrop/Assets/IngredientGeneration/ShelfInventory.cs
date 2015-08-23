using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShelfInventory : MonoBehaviour {

    // Manages access to the upcoming shelf space
    // TODO There's a final backup generator for when everyone refuses to place ingredients so the shelves aren't empty

    public int NumShelves;

    public Vector2[] SpawnPositions;

    public float xSpeed;

    // The amount of space available on each shelf
    private IList<float> shelfSpace;

    // The available strategies for ingredient placement
    private IList<IIngredientGenerator> generators;

    public ShelfInventory()
    {
        shelfSpace = new List<float>();
        generators = new List<IIngredientGenerator>();
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < NumShelves; i++)
        {
            shelfSpace.Add(0);
        }
	}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < NumShelves; i++)
        {
            shelfSpace[i] += xSpeed;
        }

        Shuffle(generators);
        foreach (IIngredientGenerator generator in generators)
        {
            for (int shelfIndex = 0; shelfIndex < NumShelves; shelfIndex++)
            {
                GameObject instantiated = generator.TryPlaceIngredient(shelfSpace, shelfIndex);
                if (instantiated != null)
                {
                    float placedWidth = instantiated.GetComponent<SpriteRenderer>().bounds.size.x;
                    shelfSpace[shelfIndex] = -placedWidth;
                    instantiated.transform.position = SpawnPositions[shelfIndex] - new Vector2(placedWidth / 2, 0);
                    instantiated.GetComponent<Rigidbody2D>().isKinematic = true;
                    instantiated.AddComponent<ConstantMovement>().Step = new Vector2(xSpeed, 0);
                }
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

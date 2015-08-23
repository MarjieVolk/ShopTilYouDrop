using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

    public IngredientType type;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = IngredientRenderer.instance().getSprite(type);
        gameObject.AddComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

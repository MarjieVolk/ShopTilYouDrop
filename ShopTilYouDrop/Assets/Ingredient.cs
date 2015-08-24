using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour {

    public IngredientType type;

	// Use this for initialization
	void Start () {
        InitializeGameObject();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitializeGameObject()
    {
        GetComponent<SpriteRenderer>().sprite = IngredientRenderer.instance().getSprite(type);
        if (gameObject.GetComponent<PolygonCollider2D>())
        {
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
        }
        gameObject.AddComponent<PolygonCollider2D>();
    }

    void OnMouseOver() {
        IngredientData data = Ingredients.instance().getIngredient(type);
        HoverInfoController.instance().showInfo(transform.position, data.getPrimaryGuess(), data.getSecondaryGuess());
    }

    void OnMouseExit() {
        HoverInfoController.instance().hideInfo();
    }

    void OnDestroy() {
        HoverInfoController.instance().hideInfo();
    }
}

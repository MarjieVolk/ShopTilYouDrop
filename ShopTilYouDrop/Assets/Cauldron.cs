using UnityEngine;
using System.Collections;

public class Cauldron : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnCollisionEnter(Collision2D collision) {
        Ingredient collidingIngredient = collision.collider.gameObject.GetComponent<Ingredient>();
        if (collidingIngredient != null) {

        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CauldronHUDController : MonoBehaviour {

    public float clearTimeSeconds;

    private float clearStartTime = -100;
    private int nFilledSlots = 0;

	// Use this for initialization
	void Start () {
        setEmptySlotAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - clearStartTime <= clearTimeSeconds) {
            setEmptySlotAlpha(1 - ((Time.time - clearStartTime) / clearTimeSeconds));
        } else {
            setEmptySlotAlpha(0);
        }
	}

    public void notifyIngredientAdded(List<IngredientType> ingredients) {
        for (int i = 0; i < ingredients.Count; i++) {
            GameObject display = transform.Find("Ingredient " + (i + 1)).gameObject;
            IngredientType type = ingredients[i];

            display.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(type);
            display.GetComponent<Image>().color = Color.white;

            IngredientData ingredient = Ingredients.instance().getIngredient(type);
            Image primaryImage = display.transform.Find("Primary Aspect").GetComponent<Image>();
            primaryImage.sprite = Aspects.instance().getNormalSprite(ingredient.getPrimaryGuess());
            primaryImage.color = Color.white;
            Image secondaryImage = display.transform.Find("Secondary Aspect").GetComponent<Image>();
            secondaryImage.sprite = Aspects.instance().getNormalSprite(ingredient.getSecondaryGuess());
            secondaryImage.color = Color.white;
        }

        nFilledSlots = ingredients.Count;
    }

    public void notifyCauldronCleared() {
        clearStartTime = Time.time;
        nFilledSlots = 0;
    }

    private void setEmptySlotAlpha(float alpha) {
        Color alphaColor = Color.white;
        alphaColor.a = alpha;

        for (int i = nFilledSlots; i < 3; i++) {
            GameObject display = transform.Find("Ingredient " + (i + 1)).gameObject;
            display.GetComponent<Image>().color = alphaColor;
            display.transform.Find("Primary Aspect").GetComponent<Image>().color = alphaColor;
            display.transform.Find("Secondary Aspect").GetComponent<Image>().color = alphaColor;
        }
    }
}

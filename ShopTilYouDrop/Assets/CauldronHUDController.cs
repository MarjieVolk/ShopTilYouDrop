using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CauldronHUDController : MonoBehaviour {

    public float clearTimeSeconds;

    private float clearStartTime = -100;
    private int nFilledSlots = 0;

    private List<Slot> slots;

	// Use this for initialization
	void Start () {
        slots = new List<Slot>();
        for (int i = 0; i < 3; i++) {
            GameObject display = transform.Find("Ingredient " + (i + 1)).gameObject;
            slots.Add(new Slot(display.GetComponent<Image>(), display.transform.Find("Primary Aspect").GetComponent<Image>(), display.transform.Find("Secondary Aspect").GetComponent<Image>()));
        }

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
        for (int i = 0; i < ingredients.Count && i < 3; i++) {
            Slot slot = slots[i];
            IngredientType type = ingredients[i];

            slot.ingredient.sprite = IngredientRenderer.instance().getSprite(type);
            slot.ingredient.color = Color.white;

            IngredientData ingredient = Ingredients.instance().getIngredient(type);
            slot.primary.sprite = Aspects.instance().getNormalSprite(ingredient.getPrimaryGuess());
            slot.primary.color = Color.white;
            slot.secondary.sprite = Aspects.instance().getNormalSprite(ingredient.getSecondaryGuess());
            slot.secondary.color = Color.white;
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

    private class Slot {
        public readonly Image ingredient;
        public readonly Image primary;
        public readonly Image secondary;

        public Slot(Image ingredient, Image primary, Image secondary) {
            this.ingredient = ingredient;
            this.primary = primary;
            this.secondary = secondary;
        }
    }

    public void showHelp() {
        GameObject.FindObjectOfType<HelpTextController>().showText("Cauldron Contents Panel", "This panel shows which ingredients are currently dissolved in your cauldron.  When three ingredients are dissolved, they will make a potion!  Potions can affect your appearance in many frightening ways.\n\nThe icons below the ingredients show your guesses for which aspects those ingredients have.  Press SPACE to find out more about aspects!");
    }
}

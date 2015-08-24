using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class IngredientMenuController : MonoBehaviour {

    public GameObject lineItem;

	void Start () {
        foreach (Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }

        foreach (IngredientType type in Enum.GetValues(typeof(IngredientType))) {
            initIngredientLineItem(type);
        }
	}

    private void initIngredientLineItem(IngredientType type) {
        GameObject display = Instantiate(lineItem);
        display.transform.SetParent(transform);

        setSprite(display, "Ingredient", IngredientRenderer.instance().getSprite(type));
        
        initAspect(type, display, "Dairy", Aspects.Primary.DAIRY);
        initAspect(type, display, "Veggie", Aspects.Primary.PLANT);
        initAspect(type, display, "Meat", Aspects.Primary.MEAT);
        initAspect(type, display, "Grain", Aspects.Primary.GRAIN);
        
        initAspect(type, display, "Fire", Aspects.Secondary.FIRE);
        initAspect(type, display, "Water", Aspects.Secondary.WATER);
        initAspect(type, display, "Void", Aspects.Secondary.VOID);
        initAspect(type, display, "Slime", Aspects.Secondary.SLIME);
        initAspect(type, display, "Decay", Aspects.Secondary.DECAY);
        initAspect(type, display, "Beast", Aspects.Secondary.BEAST);
        initAspect(type, display, "Seductive", Aspects.Secondary.SEDUCTIVE);

        display.transform.Find("Warning").GetComponent<Button>().onClick.AddListener(() => {
            showHelp();
        });
        updateWarningIcon(Ingredients.instance().getIngredient(type), display);
    }

    private void initAspect(IngredientType ingredient, GameObject lineItemInstance, String childName, Aspects.Primary aspect) {
        GameObject iconButton = lineItemInstance.transform.Find(childName).gameObject;
        iconButton.GetComponent<Image>().sprite = Aspects.instance().getGreyedSprite(aspect);

        iconButton.GetComponent<Button>().onClick.AddListener(() => {
            IngredientData data = Ingredients.instance().getIngredient(ingredient);
            IngredientData.GuessState guess = data.getGuessState(aspect);

            IngredientData.GuessState next = IngredientData.GuessState.UNKNOWN;
            Image icon = iconButton.GetComponent<Image>();
            switch (guess) {
                case IngredientData.GuessState.UNKNOWN:
                    next = IngredientData.GuessState.HAS;
                    icon.sprite = Aspects.instance().getNormalSprite(aspect);
                    break;
                case IngredientData.GuessState.HAS:
                    next = IngredientData.GuessState.NOT_HAS;
                    icon.sprite = Aspects.instance().getDisabledSprite(aspect);
                    break;
                case IngredientData.GuessState.NOT_HAS:
                    next = IngredientData.GuessState.UNKNOWN;
                    icon.sprite = Aspects.instance().getGreyedSprite(aspect);
                    break;
            }

            data.setGuessState(aspect, next);
            updateWarningIcon(data, lineItemInstance);
        });
    }

    private void initAspect(IngredientType ingredient, GameObject lineItemInstance, String childName, Aspects.Secondary aspect) {
        GameObject iconButton = lineItemInstance.transform.Find(childName).gameObject;
        iconButton.GetComponent<Image>().sprite = Aspects.instance().getGreyedSprite(aspect);

        iconButton.GetComponent<Button>().onClick.AddListener(() => {
            IngredientData data = Ingredients.instance().getIngredient(ingredient);
            IngredientData.GuessState guess = data.getGuessState(aspect);

            IngredientData.GuessState next = IngredientData.GuessState.UNKNOWN;
            Image icon = iconButton.GetComponent<Image>();
            switch (guess) {
                case IngredientData.GuessState.UNKNOWN:
                    next = IngredientData.GuessState.HAS;
                    icon.sprite = Aspects.instance().getNormalSprite(aspect);
                    break;
                case IngredientData.GuessState.HAS:
                    next = IngredientData.GuessState.NOT_HAS;
                    icon.sprite = Aspects.instance().getDisabledSprite(aspect);
                    break;
                case IngredientData.GuessState.NOT_HAS:
                    next = IngredientData.GuessState.UNKNOWN;
                    icon.sprite = Aspects.instance().getGreyedSprite(aspect);
                    break;
            }

            data.setGuessState(aspect, next);
            updateWarningIcon(data, lineItemInstance);
        });
    }

    private void setSprite(GameObject lineItemInstance, String childName, Sprite sprite) {
        lineItemInstance.transform.Find(childName).GetComponent<Image>().sprite = sprite;
    }

    private void updateWarningIcon(IngredientData data, GameObject lineItemInstance) {
        GameObject warningIcon = lineItemInstance.transform.Find("Warning").gameObject;
        warningIcon.SetActive(data.hasMultiplePrimaryGuesses() || data.hasMultipleSecondaryGuesses());
    }

    public void showHelp() {
        GameObject.FindObjectOfType<HelpTextController>().showText("Ingredients Panel", "This panel keeps track of what aspects you think each ingredient has.  Each ingredient has two aspects - a Food Group aspect and an Elemental aspect.\n\nA warning icon next to an ingredient means that you have guessed more than one Food Group aspect or more than one Elemental aspect.  Each ingredient has only one of each!");
    }
}

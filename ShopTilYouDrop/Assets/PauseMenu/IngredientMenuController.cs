﻿using UnityEngine;
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
        display.transform.parent = transform;

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
        initAspect(type, display, "Beast", Aspects.Secondary.BLOOD);
        initAspect(type, display, "Seductive", Aspects.Secondary.ANGELIC);
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
        });
    }

    private void setSprite(GameObject lineItemInstance, String childName, Sprite sprite) {
        lineItemInstance.transform.Find(childName).GetComponent<Image>().sprite = sprite;
    }
}

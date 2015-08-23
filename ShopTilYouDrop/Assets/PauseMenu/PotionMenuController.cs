using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PotionMenuController : MonoBehaviour {

    public GameObject lineItemPrefab;

    public Sprite potionSprite;
    public Sprite aspectSprite;

    public void showPotionsTab() {
        foreach (Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }

        foreach (CreatedPotion potion in Potions.instance().getLoggedPotions()) {
            GameObject potionDisplay = Instantiate(lineItemPrefab);
            potionDisplay.transform.parent = transform;

            // TODO: get correct images for potion and aspects
            List<IngredientType> ingredients = potion.getIngredients();
            potionDisplay.transform.Find("Ingredient Image 1").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
            potionDisplay.transform.Find("Ingredient Image 2").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
            potionDisplay.transform.Find("Ingredient Image 3").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
            potionDisplay.transform.Find("Potion Details/Potion Image").gameObject.GetComponent<Image>().sprite = potionSprite;
            potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 1").gameObject.GetComponent<Image>().sprite = aspectSprite;
            potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 2").gameObject.GetComponent<Image>().sprite = aspectSprite;
            potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 3").gameObject.GetComponent<Image>().sprite = aspectSprite;

            potionDisplay.transform.Find("Remove Button").GetComponent<Button>().onClick.AddListener(() => {
                Potions.instance().removeLoggedPotion(potion);
                showPotionsTab();
            });
        }
    }
}

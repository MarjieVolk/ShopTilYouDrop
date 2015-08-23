using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PotionMenuController : MonoBehaviour {

    public GameObject lineItemPrefab;
    public GameObject lineItemWithSecondariesPrefab;

    public Sprite potionSprite;
    public Sprite aspectSprite;

    public void showPotionsTab() {
        foreach (Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }

        foreach (CreatedPotion potion in Potions.instance().getLoggedPotions()) {
            GameObject potionDisplay;
            HashSet<Aspects.Secondary> secondaries = potion.getPotion().getSecondaries();
            if (secondaries.Count == 0) {
                // Potion has no secondaries
                potionDisplay = Instantiate(lineItemPrefab);
            } else {
                // Potion has secondaries
                potionDisplay = Instantiate(lineItemWithSecondariesPrefab);

                int i = 1;
                foreach (Aspects.Secondary secondary in secondaries) {
                    potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect " + i).gameObject.GetComponent<Image>().sprite = Aspects.instance().getNormalSprite(secondary);
                    i++;
                }

                while (i <= 3) {
                    Destroy(potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect " + i).gameObject);
                    i++;
                }
            }

            potionDisplay.transform.parent = transform;

            List<IngredientType> ingredients = potion.getIngredients();
            potionDisplay.transform.Find("Ingredient Image 1").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
            potionDisplay.transform.Find("Ingredient Image 2").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[1]);
            potionDisplay.transform.Find("Ingredient Image 3").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[2]);

            // TODO: get correct image for potion
            potionDisplay.transform.Find("Potion Details/Potion Image").gameObject.GetComponent<Image>().sprite = potionSprite;

            int j = 1;
            foreach (Aspects.Primary primary in potion.getPotion().getPrimaries()) {
                potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect " + j).gameObject.GetComponent<Image>().sprite = Aspects.instance().getNormalSprite(primary);
                j++;
            }

            potionDisplay.transform.Find("Remove Button").GetComponent<Button>().onClick.AddListener(() => {
                Potions.instance().removeLoggedPotion(potion);
                showPotionsTab();
            });
        }
    }
}

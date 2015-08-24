using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PotionMenuController : MonoBehaviour {

    public GameObject lineItemPrefab;
    public GameObject lineItemWithSecondariesPrefab;

    public Sprite potionSprite;

    public void showPotionsTab() {
        foreach (Transform child in gameObject.transform) {
            Destroy(child.gameObject);
        }

        foreach (CreatedPotion potion in Potions.instance().getLoggedPotions()) {
            GameObject potionDisplay;
            MultiSet<Aspects.Secondary> secondaries = potion.getPotion().getSecondaries();
            if (secondaries.Count == 0) {
                // Potion has no secondaries
                potionDisplay = Instantiate(lineItemPrefab);
            } else {
                // Potion has secondaries
                potionDisplay = Instantiate(lineItemWithSecondariesPrefab);

                int i = 0;
                foreach (Aspects.Secondary aspect in secondaries)
                {
                    GameObject icon = potionDisplay.transform.Find("Potion Details/Aspect Icons Secondary/Aspect " + (i + 1)).gameObject;
                    if (i < secondaries.Count)
                    {
                        icon.GetComponent<Image>().sprite = Aspects.instance().getNormalSprite(aspect);
                    }
                    else
                    {
                        Destroy(icon);
                    }
                    i++;
                }
            }

            potionDisplay.transform.SetParent(transform);

            List<IngredientType> ingredients = potion.getIngredients();
            for (int i = 0; i < 3; i++ ) {
                potionDisplay.transform.Find("Ingredient Image " + (i + 1)).gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[i]);
            }

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

    public void showHelp() {
        GameObject.FindObjectOfType<HelpTextController>().showText("Potions Made Panel", "This panel shows all the potions you have made, what aspects were required to make them, and what ingredients you used.  You can use this information to discover what aspects ingredients have.  Click the X button to remove potions from the list that you no longer care about.");
    }
}

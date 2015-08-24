using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PotionMenuController : MonoBehaviour {

    public GameObject lineItemPrefab;
    public GameObject lineItemWithSecondariesPrefab;

    public Sprite dudPotionSprite;

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

            Image bodyPartImage1 = potionDisplay.transform.Find("Potion Details/Potion Image").gameObject.GetComponent<Image>();
            Image bodyPartImage2 = potionDisplay.transform.Find("Potion Details/Potion Image 2").gameObject.GetComponent<Image>();

            bodyPartImage1.sprite = dudPotionSprite;
            bodyPartImage2.gameObject.SetActive(false);

            BodyPart[] parts = potion.getPotion().getAffectedBodyParts();
            if (parts.Length > 0) {
                bodyPartImage1.sprite = PlayerSprites.instance().getSprite(potion.getPotion().getType(), parts[0]);
            }
            if (parts.Length > 1) {
                bodyPartImage2.gameObject.SetActive(true);
                bodyPartImage2.sprite = PlayerSprites.instance().getSprite(potion.getPotion().getType(), parts[1]);
            }

            int j = 1;
            foreach (Aspects.Primary primary in potion.getPotion().getPrimaries()) {
                potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect " + j).gameObject.GetComponent<Image>().sprite = Aspects.instance().getNormalSprite(primary);
                j++;
            }

            CreatedPotion potion2 = potion;
            potionDisplay.transform.Find("Remove Button").GetComponent<Button>().onClick.AddListener(() => {
                Potions.instance().removeLoggedPotion(potion2);
                showPotionsTab();
            });
        }
    }

    public void showHelp() {
        GameObject.FindObjectOfType<HelpTextController>().showText("Potions Made Panel", "This panel shows all the potions you have made, what aspects were required to make them, and what ingredients you used.  Can you figure out which ingredients have which aspects?\n\nClick the X button to remove potions from the list that you no longer care about.");
    }
}

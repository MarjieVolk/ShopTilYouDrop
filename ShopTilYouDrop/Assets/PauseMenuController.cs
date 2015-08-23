using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour {

    public CanvasRenderer pauseMenu;
    public CanvasRenderer potionsParent;
    public CanvasRenderer ingredientsContent;

    public Sprite potionSprite;
    public Sprite ingredientSprite;
    public Sprite aspectSprite;

    public GameObject lineItemPrefab;

	// Use this for initialization
    void Start() {
        pauseMenu.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);

            foreach (Transform child in potionsParent.gameObject.transform) {
                Destroy(child.gameObject);
            }

            foreach (CreatedPotion potion in Potions.instance().getLoggedPotions()) {
                GameObject potionDisplay = Instantiate(lineItemPrefab);
                potionDisplay.transform.parent = potionsParent.transform;

                // TODO: get correct images for ingrediants, potion, and aspects
                List<IngredientType> ingredients = potion.getIngredients();
                potionDisplay.transform.Find("Ingredient Image 1").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
                potionDisplay.transform.Find("Ingredient Image 2").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
                potionDisplay.transform.Find("Ingredient Image 3").gameObject.GetComponent<Image>().sprite = IngredientRenderer.instance().getSprite(ingredients[0]);
                potionDisplay.transform.Find("Potion Details/Potion Image").gameObject.GetComponent<Image>().sprite = potionSprite;
                potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 1").gameObject.GetComponent<Image>().sprite = aspectSprite;
                potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 2").gameObject.GetComponent<Image>().sprite = aspectSprite;
                potionDisplay.transform.Find("Potion Details/Aspect Icons/Aspect 3").gameObject.GetComponent<Image>().sprite = aspectSprite;
            }
        }
	}

    public void onClickResume() {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
}

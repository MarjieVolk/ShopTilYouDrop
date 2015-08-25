using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour {

    private PotionMenuController potionMenu;
    private IngredientMenuController ingredientMenu;
    private GameObject pauseMenu;
    private AudioSwitcher pauser;

	// Use this for initialization
    void Start() {
        potionMenu = GameObject.FindObjectOfType<PotionMenuController>();
        ingredientMenu = GameObject.FindObjectOfType<IngredientMenuController>();
        pauser = GameObject.FindObjectOfType<AudioSwitcher>();
        pauseMenu = transform.Find("Pause Menu").gameObject;
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) && !pauseMenu.activeInHierarchy) {
            pause();
        }
	}

    public void pause()
    {
        pauser.AddPause();
        pauseMenu.SetActive(true);
        potionMenu.showPotionsTab();
        ingredientMenu.showIngredientTab();
    }

    public void onClickResume() {
        pauser.RemovePause();
        pauseMenu.SetActive(false);
    }

    public void onClickExit() {
        Application.Quit();
    }
}

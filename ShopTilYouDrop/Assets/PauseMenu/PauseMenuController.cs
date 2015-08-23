using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour {

    private PotionMenuController potionMenu;
    private GameObject pauseMenu;

	// Use this for initialization
    void Start() {
        potionMenu = GameObject.FindObjectOfType<PotionMenuController>();
        pauseMenu = transform.Find("Pause Menu").gameObject;
        pauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            potionMenu.showPotionsTab();
        }
	}

    public void onClickResume() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void showIngredientsTab() {
        
    }
}

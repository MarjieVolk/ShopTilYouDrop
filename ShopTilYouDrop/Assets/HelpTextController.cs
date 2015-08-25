﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTextController : MonoBehaviour {

    private GameObject tutorialPanel;
    private Text title, body;
    private AudioSwitcher pauser;
    private bool isOpen = false;

	void Start () {
        pauser = GameObject.FindObjectOfType<AudioSwitcher>();
        tutorialPanel = transform.Find("Help Text").gameObject;
        tutorialPanel.SetActive(false);
        title = tutorialPanel.transform.Find("Title").GetComponent<Text>();
        body = tutorialPanel.transform.Find("Body").GetComponent<Text>();

        tutorialPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            if (isOpen == true)
            {
                pauser.RemovePause();
                isOpen = false;
            }
            tutorialPanel.SetActive(false);
        });

        showText("You Are What You Eat", "As the most powerful wizard in town, you usually spend your days brewing potions in your tower.  But this morning you realized you had run out of ingredients!  Guess you'll have to make do with whatever they have at the local grocery store.");
	}

    public void showText(string title, string body) {
        this.title.text = title;
        this.body.text = body;
        tutorialPanel.SetActive(true);
        if (isOpen == false)
        {
            pauser.AddPause();
            isOpen = true;
        }
    }
}

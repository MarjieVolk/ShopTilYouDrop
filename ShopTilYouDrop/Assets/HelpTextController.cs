using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelpTextController : MonoBehaviour {

    private GameObject tutorialPanel;
    private Text title, body;

	void Start () {
        tutorialPanel = transform.Find("Help Text").gameObject;
        tutorialPanel.SetActive(false);
        title = tutorialPanel.transform.Find("Title").GetComponent<Text>();
        body = tutorialPanel.transform.Find("Body").GetComponent<Text>();

        tutorialPanel.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            tutorialPanel.SetActive(false);
        });
	}

    public void showText(string title, string body) {
        this.title.text = title;
        this.body.text = body;
        tutorialPanel.SetActive(true);
    }
}

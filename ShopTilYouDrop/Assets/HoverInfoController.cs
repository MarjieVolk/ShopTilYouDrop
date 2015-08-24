using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverInfoController : MonoBehaviour {

    private static HoverInfoController INSTANCE;

    public static HoverInfoController instance() {
        return INSTANCE;
    }

    private GameObject panel;
    private Image primary, secondary;

	// Use this for initialization
	void Start () {
        INSTANCE = this;

        panel = transform.Find("Hover Info").gameObject;
        primary = panel.transform.Find("Primary").GetComponent<Image>();
        secondary = panel.transform.Find("Secondary").GetComponent<Image>();

        hideInfo();
	}

    public void showInfo(Vector3 location, Aspects.Primary primary, Aspects.Secondary secondary) {
        panel.transform.position = new Vector3(location.x, location.y, panel.transform.position.z);
        this.primary.sprite = Aspects.instance().getNormalSprite(primary);
        this.secondary.sprite = Aspects.instance().getNormalSprite(secondary);
        panel.SetActive(true);
    }

    public void hideInfo() {
        if (panel != null) {
            panel.SetActive(false);
        }
    }
}

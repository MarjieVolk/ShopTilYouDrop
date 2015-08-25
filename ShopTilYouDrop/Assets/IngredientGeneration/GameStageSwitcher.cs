using UnityEngine;
using System.Collections;
using System;

public class GameStageSwitcher : MonoBehaviour {

    public GameObject stageOneGenerators, stageTwoGenerators, stageThreeGenerators;

    private int currentStage = 1;
    private GameObject generators;
    private PlayerSpriteController playerSpriteController;

    private int numAdvParts;
    private int numChParts;

	// Use this for initialization
	void Start () {
        generators = Instantiate(stageOneGenerators);
        playerSpriteController = FindObjectOfType<PlayerSpriteController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (currentStage == 1 && switchToStageTwo()) {
            currentStage++;
            Destroy(generators);
            generators = Instantiate(stageTwoGenerators);
        } else if (currentStage == 2 && switchToStageThree()) {
            currentStage++;
            Destroy(generators);
            generators = Instantiate(stageTwoGenerators);
        }
        numAdvParts = numAdvancedPotionSlots();
        numChParts = numChangedPotionSlots();
	}

    private bool switchToStageTwo() {
        return numChangedPotionSlots() >= 3;
    }

    private bool switchToStageThree() {
        return numAdvancedPotionSlots() >= 3;
    }

    private int numAdvancedPotionSlots()
    {
        int ret = 0;
        foreach (PotionSlot potionSlot in Enum.GetValues(typeof(PotionSlot)))
        {
            Aspects.Secondary? aspect = playerSpriteController.GetAspectForPotionSlot(potionSlot);
            if (aspect != null && aspect != Aspects.Secondary.NONE && aspect != Aspects.Secondary.BASIC && aspect != Aspects.Secondary.UNKNOWN)
            {
                ret++;
            }
        }

        return ret;
    }

    private int numChangedPotionSlots()
    {
        int ret = 0;
        foreach (PotionSlot slot in Enum.GetValues(typeof(PotionSlot)))
        {
            Aspects.Secondary? aspect = playerSpriteController.GetAspectForPotionSlot(slot);
            if (aspect != null && aspect != Aspects.Secondary.UNKNOWN)
            {
                ret++;
            }
        }

        return ret;
    }
}

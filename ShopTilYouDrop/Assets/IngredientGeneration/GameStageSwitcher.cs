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
            generators = Instantiate(stageThreeGenerators);
        }
        numAdvParts = numAdvancedBodyParts();
        numChParts = numChangedBodyParts();
	}

    private bool switchToStageTwo() {
        return numChangedBodyParts() >= 3;
    }

    private bool switchToStageThree() {
        return numAdvancedBodyParts() >= 3;
    }

    private int numAdvancedBodyParts()
    {
        int ret = 0;
        foreach (BodyPart part in Enum.GetValues(typeof(BodyPart)))
        {
            Aspects.Secondary? aspect = playerSpriteController.GetAspectForBodyPart(part);
            if (aspect != null && aspect != Aspects.Secondary.NONE && aspect != Aspects.Secondary.BASIC && aspect != Aspects.Secondary.UNKNOWN)
            {
                ret++;
            }
        }

        return ret;
    }

    private int numChangedBodyParts()
    {
        int ret = 0;
        foreach (BodyPart part in Enum.GetValues(typeof(BodyPart)))
        {
            Aspects.Secondary? aspect = playerSpriteController.GetAspectForBodyPart(part);
            if (aspect != null && aspect != Aspects.Secondary.UNKNOWN)
            {
                ret++;
            }
        }

        return ret;
    }
}

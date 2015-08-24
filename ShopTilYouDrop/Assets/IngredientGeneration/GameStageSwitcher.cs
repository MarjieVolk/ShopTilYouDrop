using UnityEngine;
using System.Collections;

public class GameStageSwitcher : MonoBehaviour {

    public GameObject stageOneGenerators, stageTwoGenerators, stageThreeGenerators;

    private int currentStage = 1;
    private GameObject generators;

	// Use this for initialization
	void Start () {
        generators = Instantiate(stageOneGenerators);
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
	}

    private bool switchToStageTwo() {
        return false;
    }

    private bool switchToStageThree() {
        return false;
    }
}

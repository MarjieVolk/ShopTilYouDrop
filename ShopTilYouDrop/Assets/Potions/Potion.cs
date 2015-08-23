using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public class Potion {
    private static PlayerSpriteController _playerSpriteController;
    private static PlayerSpriteController playerSpriteController
    {
        get
        {
            if (_playerSpriteController == null)
            {
                _playerSpriteController = GameObject.FindObjectOfType<PlayerSpriteController>();
            }
            return _playerSpriteController;
        }
    }

    private List<Aspects.Primary> primaries;
    private List<Aspects.Secondary> secondaries;
    private PotionSlot _slot;
    private Aspects.Secondary _type;
    private Effect _effect;

    public Potion(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3,
                  List<Aspects.Secondary> secondaries, PotionSlot slot, Aspects.Secondary type, Effect effect) {
        primaries = new List<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);
        primaries.Sort();

        this.secondaries = secondaries;
        secondaries.Sort();

        _slot = slot;
        _type = type;
        _effect = effect;
    }

    public List<Aspects.Primary> getPrimaries() {
        return new List<Aspects.Primary>(primaries);
    }

    public List<Aspects.Secondary> getSecondaries() {
        return new List<Aspects.Secondary>(secondaries);
    }

    public void TriggerEffect() {
        BodyPart[] bodyParts = _slot.ToBodyParts();
        playerSpriteController.setBodyParts(bodyParts, _type);
        playerSpriteController.setEffect(_slot, _effect);
    }
}

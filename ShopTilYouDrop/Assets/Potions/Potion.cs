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

    private MultiSet<Aspects.Primary> primaries;
    private MultiSet<Aspects.Secondary> secondaries;
    private PotionSlot _slot;
    private Aspects.Secondary _type;
    private Effect _effect;

    public Potion(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3,
                  List<Aspects.Secondary> secondaries, PotionSlot slot, Aspects.Secondary type, Effect effect) {
        primaries = new MultiSet<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);

        this.secondaries = new MultiSet<Aspects.Secondary>(secondaries);

        _slot = slot;
        _type = type;
        _effect = effect;
    }

    public MultiSet<Aspects.Primary> getPrimaries() {
        return new MultiSet<Aspects.Primary>(primaries);
    }

    public MultiSet<Aspects.Secondary> getSecondaries()
    {
        return new MultiSet<Aspects.Secondary>(secondaries);
    }

    public void TriggerEffect() {
        BodyPart[] bodyParts = _slot.ToBodyParts();
        playerSpriteController.setBodyParts(bodyParts, _type);
        playerSpriteController.setEffect(_slot, _effect);
    }
}

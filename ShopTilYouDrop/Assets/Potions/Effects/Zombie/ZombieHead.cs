using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ZombieHead : Effect {
    public override void Trigger() {
        // Give the player a zombie head.
        // Trigger an immediate "moan" sound.
        // Trigger a "moan" effect that scares bystanders.
    }

    public override void UnTrigger()
    {
        throw new NotImplementedException();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ZombieNap : Effect {
    public override void Trigger() {
        // Trigger effect where player "plays dead" and bystanders lose interest.
    }

    public override void UnTrigger()
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Potion {

    private HashSet<Aspects.Primary> primaries;
    private HashSet<Aspects.Secondary> secondaries;

    public Potion(Aspects.Primary primary1, Aspects.Primary primary2, Aspects.Primary primary3,
                  HashSet<Aspects.Secondary> secondaries) {
        primaries = new HashSet<Aspects.Primary>();
        primaries.Add(primary1);
        primaries.Add(primary2);
        primaries.Add(primary3);

        this.secondaries = secondaries;
    }

    public HashSet<Aspects.Primary> getPrimaries() {
        return new HashSet<Aspects.Primary>(primaries);
    }

    public HashSet<Aspects.Secondary> getSecondaries() {
        return new HashSet<Aspects.Secondary>(secondaries);
    }

    public void triggerEffect() {
        // Send a potion message?
        //effect.trigger();
    }
}

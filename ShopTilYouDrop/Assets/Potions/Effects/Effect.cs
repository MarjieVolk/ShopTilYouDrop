using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract public class Effect {

    public Effect() {
    }

    public abstract void Trigger();

    public abstract void UnTrigger();
}

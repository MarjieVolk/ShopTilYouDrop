using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

abstract public class Effect : MonoBehaviour{

    public Effect() {
    }

    public abstract void Trigger();

    public abstract void UnTrigger();
}

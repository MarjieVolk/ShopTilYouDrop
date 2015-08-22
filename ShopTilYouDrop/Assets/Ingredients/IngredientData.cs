using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class IngredientData {
    public readonly IngredientType type;
    public readonly Aspects.Primary primary;
    public readonly Aspects.Secondary secondary;

    public Aspects.Primary primaryPlayerGuess = Aspects.Primary.UNKNOWN;
    public Aspects.Secondary secondaryPlayerGuess = Aspects.Secondary.UNKNOWN;

    public IngredientData(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        this.type = type;
        this.primary = primary;
        this.secondary = secondary;
    }
}

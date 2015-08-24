using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IIngredientGenerator
{
    IngredientType? TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex);
}

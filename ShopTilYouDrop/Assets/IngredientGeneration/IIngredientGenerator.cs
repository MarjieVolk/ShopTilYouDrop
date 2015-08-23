using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IIngredientGenerator
{
    GameObject TryPlaceIngredient(IList<float> shelfSpace, int shelfIndex);
}

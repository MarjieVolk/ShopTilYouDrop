using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum IngredientType {
    CHEDDAR, LETTUCE, BEEF, WHITE_BREAD,
    SWISS, BELL_PEPPER, TOFU, NAAN,
    ICE_CREAM, PEACH, BACON, DONUT,
    PEPPER_JACK, HABANERO, FRIED_CHICKEN, TOASTED_BREAD,
    MILK, APPLE, FISH, RICE,
    YOGURT, MUSHY_PEAS, CALAMARI, SPAGHETTI,
    STINKY_CHEESE, MUSHROOMS, SPOILED_MEAT, BEER,
    GREEN_CHEESE, TOMATO, RAW_MEAT, DOG_BISCUIT
}

public static class IngredientTypeExtensions
{
    public static Aspects.Primary GetPrimaryAspect(this IngredientType ingredient)
    {
        switch (ingredient)
        {
            case IngredientType.CHEDDAR:
            case IngredientType.SWISS:
            case IngredientType.ICE_CREAM:
            case IngredientType.PEPPER_JACK:
            case IngredientType.MILK:
            case IngredientType.YOGURT:
            case IngredientType.STINKY_CHEESE:
            case IngredientType.GREEN_CHEESE:
                return Aspects.Primary.DAIRY;
            case IngredientType.LETTUCE:
            case IngredientType.BELL_PEPPER:
            case IngredientType.PEACH:
            case IngredientType.HABANERO:
            case IngredientType.APPLE:
            case IngredientType.MUSHY_PEAS:
            case IngredientType.MUSHROOMS:
            case IngredientType.TOMATO:
                return Aspects.Primary.PLANT;
            case IngredientType.BEEF:
            case IngredientType.TOFU:
            case IngredientType.BACON:
            case IngredientType.FRIED_CHICKEN:
            case IngredientType.FISH:
            case IngredientType.CALAMARI:
            case IngredientType.SPOILED_MEAT:
            case IngredientType.RAW_MEAT:
                return Aspects.Primary.MEAT;
            case IngredientType.WHITE_BREAD:
            case IngredientType.NAAN:
            case IngredientType.DONUT:
            case IngredientType.TOASTED_BREAD:
            case IngredientType.RICE:
            case IngredientType.SPAGHETTI:
            case IngredientType.BEER:
            case IngredientType.DOG_BISCUIT:
                return Aspects.Primary.GRAIN;
            default:
                throw new ArgumentException("Unrecognized IngredientType: " + ingredient);
        }
    }

    public static Aspects.Secondary GetSecondaryAspect(this IngredientType ingredient)
    {
        switch (ingredient)
        {
            case IngredientType.CHEDDAR:
            case IngredientType.LETTUCE:
            case IngredientType.BEEF:
            case IngredientType.WHITE_BREAD:
                return Aspects.Secondary.NONE;
            case IngredientType.SWISS:
            case IngredientType.BELL_PEPPER:
            case IngredientType.TOFU:
            case IngredientType.NAAN:
                return Aspects.Secondary.VOID;
            case IngredientType.ICE_CREAM:
            case IngredientType.PEACH:
            case IngredientType.BACON:
            case IngredientType.DONUT:
                return Aspects.Secondary.SEDUCTIVE;
            case IngredientType.PEPPER_JACK:
            case IngredientType.HABANERO:
            case IngredientType.FRIED_CHICKEN:
            case IngredientType.TOASTED_BREAD:
                return Aspects.Secondary.FIRE;
            case IngredientType.MILK:
            case IngredientType.APPLE:
            case IngredientType.FISH:
            case IngredientType.RICE:
                return Aspects.Secondary.WATER;
            case IngredientType.YOGURT:
            case IngredientType.MUSHY_PEAS:
            case IngredientType.CALAMARI:
            case IngredientType.SPAGHETTI:
                return Aspects.Secondary.SLIME;
            case IngredientType.STINKY_CHEESE:
            case IngredientType.MUSHROOMS:
            case IngredientType.SPOILED_MEAT:
            case IngredientType.BEER:
                return Aspects.Secondary.DECAY;
            case IngredientType.GREEN_CHEESE:
            case IngredientType.TOMATO:
            case IngredientType.RAW_MEAT:
            case IngredientType.DOG_BISCUIT:
                return Aspects.Secondary.BEAST;
            default:
                throw new ArgumentException("Unrecognized IngredientType: " + ingredient);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class IngredientData {
    public readonly IngredientType type;
    public readonly Aspects.Primary primary;
    public readonly Aspects.Secondary secondary;

    private Dictionary<Aspects.Primary, GuessState> primaryGuesses;
    private Dictionary<Aspects.Secondary, GuessState> secondaryGuesses;

    public IngredientData(IngredientType type, Aspects.Primary primary, Aspects.Secondary secondary) {
        this.type = type;
        this.primary = primary;
        this.secondary = secondary;

        primaryGuesses = new Dictionary<Aspects.Primary, GuessState>();
        secondaryGuesses = new Dictionary<Aspects.Secondary, GuessState>();
    }

    public GuessState getGuessState(Aspects.Primary aspect) {
        if (primaryGuesses.ContainsKey(aspect)) {
            return primaryGuesses[aspect];
        } else {
            return GuessState.UNKNOWN;
        }
    }

    public GuessState getGuessState(Aspects.Secondary aspect) {
        if (secondaryGuesses.ContainsKey(aspect)) {
            return secondaryGuesses[aspect];
        } else {
            return GuessState.UNKNOWN;
        }
    }

    public void setGuessState(Aspects.Primary aspect, GuessState state) {
        primaryGuesses[aspect] = state;
    }

    public void setGuessState(Aspects.Secondary aspect, GuessState state) {
        secondaryGuesses[aspect] = state;
    }

    public Aspects.Primary getPrimaryGuess() {
        Aspects.Primary guess = Aspects.Primary.UNKNOWN;
        foreach (Aspects.Primary aspect in primaryGuesses.Keys) {
            if (primaryGuesses[aspect] == GuessState.HAS) {
                if (guess == Aspects.Primary.UNKNOWN) {
                    guess = aspect;
                } else {
                    // Multiple aspects guessed; invalid
                    return Aspects.Primary.UNKNOWN;
                }
            }
        }

        return guess;
    }

    public bool hasMultiplePrimaryGuesses() {
        Aspects.Primary guess = Aspects.Primary.UNKNOWN;
        foreach (Aspects.Primary aspect in primaryGuesses.Keys) {
            if (primaryGuesses[aspect] == GuessState.HAS) {
                if (guess == Aspects.Primary.UNKNOWN) {
                    guess = aspect;
                } else {
                    // Multiple aspects guessed
                    return true;
                }
            }
        }

        return false;
    }

    public Aspects.Secondary getSecondaryGuess() {
        Aspects.Secondary guess = Aspects.Secondary.UNKNOWN;
        foreach (Aspects.Secondary aspect in secondaryGuesses.Keys) {
            if (secondaryGuesses[aspect] == GuessState.HAS) {
                if (guess == Aspects.Secondary.UNKNOWN) {
                    guess = aspect;
                } else {
                    // Multiple aspects guessed; invalid
                    return Aspects.Secondary.UNKNOWN;
                }
            }
        }

        return guess;
    }

    public bool hasMultipleSecondaryGuesses() {
        Aspects.Secondary guess = Aspects.Secondary.UNKNOWN;
        foreach (Aspects.Secondary aspect in secondaryGuesses.Keys) {
            if (secondaryGuesses[aspect] == GuessState.HAS) {
                if (guess == Aspects.Secondary.UNKNOWN) {
                    guess = aspect;
                } else {
                    // Multiple aspects guessed
                    return true;
                }
            }
        }

        return false;
    }

    public enum GuessState {
        UNKNOWN,
        HAS,
        NOT_HAS
    }
}

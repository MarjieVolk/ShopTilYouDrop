using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Extensions
{
    public static int getFirstSet(this LayerMask mask)
    {
        int value = mask.value;
        for (int i = 31; i >= 0; i--) // 32 because fuck you why do I have to write this (no actually Unity only permits 32 total layers...)
        {
            value = value << 1;
            if (value == 0) return i;
        }
        throw new ArgumentException("Invalid mask with value " + mask.value);
    }
}

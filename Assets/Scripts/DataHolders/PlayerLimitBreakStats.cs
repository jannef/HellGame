using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitBreakStats : ScriptableObject {

    public int BreakPointLimit = 50;
    public float LimitBreakLenght = 3f;
    [SerializeField] private int[] BreakPointLimitIncreases;
    private int currentBreakPointLimitIndex = 0;

    public int GetLatestBreakPointIncrease()
    {
        var returnableValue =
            BreakPointLimitIncreases[Mathf.Clamp(currentBreakPointLimitIndex, 0, BreakPointLimitIncreases.Length -1)];
        currentBreakPointLimitIndex++;
        return returnableValue;
    }
}

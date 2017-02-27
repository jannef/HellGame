using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimitBreakStats : ScriptableObject {
    public int Cost = 50;
    public float DesiredBaseLenght = 3f;
    public float CostMultiplierPerSecond = 1.2f;
    public int GetLatestBreakPointIncrease { get { return BreakPointLimitIncreases.Length == 0 ? 0 : BreakPointLimitIncreases[Mathf.Clamp(_currentIndex++, 0, BreakPointLimitIncreases.Length - 1)]; } }
    [SerializeField] private int[] BreakPointLimitIncreases;
    private int _currentIndex = 0;
}

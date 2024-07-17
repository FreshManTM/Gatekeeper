using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName ="Level")]
public class LevelInfo: ScriptableObject
{
    public int LevelNumber;
    public int LevelTime;
    public int StrikerWavesAmount;
    public int MaxStrikerAmountPerWave;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Scriptable/2D_Array/ScoreData")]
public class Grid_ScoreData : ScriptableObject
{
    public int CrossPlayerScore;
    public int CirclePlayerScore;
    public bool NotCrossPlayerStarts;
}

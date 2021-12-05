using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseLevel
{
    public string levelName;
    public int id;
    public bool isEndOfGame;
    public List<ObstacleLayout> layout;
}

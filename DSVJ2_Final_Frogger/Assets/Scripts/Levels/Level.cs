using UnityEngine;

[CreateAssetMenu(fileName ="LevelData", menuName ="Levels/NewLevel", order = 1)]
public class Level : ScriptableObject
{
    public BaseLevel levelData;
}

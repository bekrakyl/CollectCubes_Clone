using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "Level/LevelScriptable")]
public class LevelScriptable : ScriptableObject
{
    [AssetList(Path = "Prefabs/LevelPrefabs")]
    public GameObject levelPrefab;
    public LevelType levelType;

    [ShowIf("@levelType != LevelType.DefaultLevel")] public int levelCubeLimit;
    [ShowIf("@levelType != LevelType.DefaultLevel")] public int startTimeMinute;
}

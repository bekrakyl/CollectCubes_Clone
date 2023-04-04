using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelScriptable level;

    private void Awake()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        int levelIndex = PrefManager.GetLevel;
        LevelScriptable levelScriptable = Resources.Load(StringUtil.LEVEL_SCRIPTABLE_PATH + levelIndex) as LevelScriptable;

        if (levelScriptable == null)
        {
            int allLevelsCount = Resources.LoadAll<LevelScriptable>("Levels").Length;
            levelIndex = ((PrefManager.GetLevel - 1) % allLevelsCount) + 1;

            levelScriptable = Resources.Load(StringUtil.LEVEL_SCRIPTABLE_PATH + levelIndex) as LevelScriptable;
        }

        LevelController level = Instantiate(levelScriptable.levelPrefab).GetComponent<LevelController>();
        level.ActiveLevelScriptable = levelScriptable;
        this.level = levelScriptable;
    }
}

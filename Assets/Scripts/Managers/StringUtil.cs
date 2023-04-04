using System.Linq;
using UnityEngine;

public class StringUtil
{
    public const string PREF_LEVEL = "LevelIndex";
    public const string LEVEL_SCRIPTABLE_PATH = "Levels/Level ";
    public const string SCENE_NAME = "Main";
    public const string LAYER_IGNORESELF = "IgnoreSelf";
}
public static class GO_Extensions
{
    public static void CenterOnChildred(this Transform aParent)
    {
        var childs = aParent.Cast<Transform>().ToList();
        var pos = new Vector3(0, 0.5f, 0);
        foreach (var C in childs)
        {
            pos += C.position;
            C.parent = null;
        }
        pos /= childs.Count;
        aParent.position = pos;
        foreach (var C in childs)
            C.parent = aParent;
    }

    public static void SetLayer(this GameObject obj, string layer)
    {
        int layerIndex = LayerMask.NameToLayer(layer);
        obj.layer = layerIndex; 

    }

}

using UnityEngine;

[System.Serializable]
public struct MaterialSet
{

}

[CreateAssetMenu(menuName = "Managers/MaterialManager", fileName = "MaterialManager.asset")]
public class MaterialManager : ScriptableObject
{
    [SerializeField] private Material[] cubeMaterials;
    [SerializeField] private Material collectedCubeMat;

    public Material GetCubeMat(int index)
    {
        return index > 0 ? cubeMaterials[index] : cubeMaterials.RandomAt();
    }

    public Material CollectedMat() => collectedCubeMat;

}

#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelCreator.asset", menuName = "Level/LevelCreator")]
public class LevelEditor : ScriptableObject
{
    private int PixelFrequency { get => Mathf.Clamp(pixelFrequency, 1, 500); }
    [SerializeField] private Texture2D image;
    [SerializeField, ShowIf("@image != null")] private int pixelFrequency;
    [SerializeField, ShowIf("@image != null")] private GameObject cubePrefab;




    [Button]
    public void CreateMap()
    {
        if (image == null) return;
        GameObject cubesParent = new GameObject("CubesParent");

        for (int i = 0; i < image.width; i += PixelFrequency)
            for (int j = 0; j < image.height; j += PixelFrequency)
                CreatePart(i, j, cubesParent.transform);

        cubesParent.transform.CenterOnChildred();
        cubesParent.transform.position = Vector3.zero;
        cubesParent.AddComponent<CubesManager>();
        Debug.Log(cubesParent.transform.childCount);
    }

    private void CreatePart(int i, int j, Transform cubesParent)
    {
        float posFrequency = pixelFrequency * .985f;
        Vector3 cubePos = new Vector3(i / posFrequency, 1f, j / posFrequency);
        Color pixelColor = image.GetPixel(i, j);

        //GameObject cube = ActionManager.GetItemFromPool(PoolType.Cube, cubePos, cubesParent);
        GameObject cube = PrefabUtility.InstantiatePrefab(cubePrefab, cubesParent) as GameObject;
        cube.transform.position = cubePos;
        cube.GetComponent<Cube>().SetCubeColor(pixelColor);
    }
}
#endif
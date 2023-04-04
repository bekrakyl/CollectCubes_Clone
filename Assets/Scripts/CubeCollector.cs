using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class CubeCollector : MonoBehaviour, ICollisionCollector
{
    [SerializeField] private TextMeshPro collectedText;
    [SerializeField] private bool isPlayerCollector = true;

    private LevelType levelType;



    private int collectedCount = 0;

    private void Start()
    {
        levelType = GetComponentInParent<LevelController>().ActiveLevelScriptable.levelType;
        if (collectedText != null)
            collectedText.text = collectedCount.ToString();
    }

    public void OnInteractEnter(Cube cube)
    {
        Vector3 cubePos = transform.position;
        cubePos.x += Random.Range(-4.5f, 4.5f);
        cubePos.y += cube.transform.GetChild(0).localScale.y / 2;
        cubePos.z += Random.Range(-2f, 3f);

        bool returnPool = levelType == LevelType.DefaultLevel ? false : true;
        cubePos = returnPool ? transform.position : cubePos;

        cube.CollectEnd(cubePos, returnPool);

        if (collectedText != null)
        {
            collectedCount++;
            collectedText.text = collectedCount.ToString();
            if (isPlayerCollector)
                ActionManager.CubeCollected?.Invoke(collectedCount);
        }
    }
}

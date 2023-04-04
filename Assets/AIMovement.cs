using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] private Transform collectorModel;
    [SerializeField] private CubesManager cubesManager;
    [SerializeField] private CubeCollector cubeCollector;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        cubesManager = transform.parent.GetComponentInChildren<CubesManager>();

        gameManager.GameStart += GameStart;
    }

    private void GameStart()
    {
        Move();
    }

    void Move()
    {
        Transform moveTr = cubeCollector.transform;
        int possibility = Random.Range(0, 101);
        if (possibility < 55)
            moveTr = cubesManager.GetRandomCubeTr();

        float time = .2f;
        float distance = Vector3.Distance(transform.position, moveTr.position);
        GoToCube(moveTr, distance, time);
    }

    private void GoToCube(Transform moveTr, float distance, float time)
    {
        Vector3 movePos = new Vector3(moveTr.position.x, transform.position.y, moveTr.position.z);
        transform.DOLookAt(movePos, time).SetEase(Ease.OutQuad).SetId(GetHashCode());
        transform.DOMove(movePos, time * distance)
            .SetEase(Ease.Linear)
            .SetId(GetHashCode())
            .OnComplete(() => Move());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameManager.ExecuteGame) return;
        other.gameObject.GetComponent<ICollisionCube>()?.OnInteractEnter("AI");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!gameManager.ExecuteGame) return;
        other.gameObject.GetComponent<ICollisionCube>()?.OnInteractExit();
    }
}
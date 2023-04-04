using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour, ICollisionCube
{
    [SerializeField] private Color cubeColor;

    private CubesManager cubesManager;
    private Managers managers;

    private MeshRenderer cubeRenderer;

    private Rigidbody cubeBody;
    private Collider cubeCollider;

    private bool canInteract = true;


    private void Awake()
    {

        cubeRenderer = GetComponentInChildren<MeshRenderer>();
        cubeCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        managers = Managers.Instance;

        cubesManager = GetComponentInParent<CubesManager>();
        cubesManager.AddCubes(this);

        transform.DOScale(Vector3.zero, 1f).From()
            .SetEase(Ease.OutBounce)
            .SetId(GetHashCode());

        cubeRenderer.material = managers.MaterialManager.GetCubeMat(-1);

        LevelType levelType = transform.parent.GetComponentInParent<LevelController>().ActiveLevelScriptable.levelType;
        if (levelType == LevelType.DefaultLevel)
            if (cubeColor.a == 0)
            {
                cubesManager.OnCubeCollected(this);
                Destroy(gameObject);
                return;
            }
            else
                cubeRenderer.material.SetColor("_Color", cubeColor);

        GameManager.Instance.GameStart += GameStart;
    }

    private void GameStart()
    {

        cubeCollider.enabled = false;
        cubeCollider.enabled = true;
    }

    public void OnInteractEnter(string layer)
    {
        if (!canInteract) return;

        gameObject.SetLayer(layer);

        if (cubeBody == null)
            cubeBody = gameObject.AddComponent<Rigidbody>();

        cubeBody.mass = .2f;

        canInteract = false;
    }

    public void OnInteractExit()
    {
        gameObject.SetLayer("Default");

        RunExtension.After(3f, () =>
        {
            if (canInteract)
            {
                Destroy(cubeBody);
                cubeBody = null;
            }
        });
        canInteract = true;
    }

    public void CollectEnd(Vector3 cubePos, bool returnPool = false)
    {
        cubesManager.OnCubeCollected(this);

        transform.DOMove(cubePos, .75f).SetEase(Ease.OutBounce).SetId(GetHashCode()).OnComplete(() =>
        {
            if (returnPool)
                ActionManager.ReturnItemToPool?.Invoke(gameObject, PoolType.Cube);
        });

        Destroy(cubeBody);

        if (!returnPool)
        {
            cubeRenderer.material = managers.MaterialManager.CollectedMat();
            Destroy(cubeCollider);
            Destroy(this);
        }
        else
        {
            cubeCollider.enabled = false;
        }
    }

    public void SetCubeColor(Color color)
    {
        cubeColor = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<ICollisionCollector>()?.OnInteractEnter(this);
    }

    public void OnComePool()
    {
        transform.eulerAngles = Vector3.zero;
        cubeCollider.enabled = true;
        canInteract = true;
        OnInteractEnter("Default");
        OnInteractExit();
    }
}
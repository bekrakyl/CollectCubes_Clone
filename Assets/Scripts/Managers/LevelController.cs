using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class LevelController : MonoBehaviour
{
    public LevelScriptable ActiveLevelScriptable { get => activeLevelScriptable; set => activeLevelScriptable = value; }

    [SerializeField] private Transform cubesParent;

    private LevelScriptable activeLevelScriptable;

    private GameManager gameManager;
    private CanvasManager canvasManager;

    private int minute;
    private float second;

    private int collectedCubeCount;

    private bool gameActive = false;

    private void Start()
    {
        gameActive = true;

        gameManager = GameManager.Instance;
        canvasManager = CanvasManager.Instance;

        if (activeLevelScriptable.levelType != LevelType.DefaultLevel)
        {
            minute = activeLevelScriptable.startTimeMinute;
            canvasManager.OpenTimer(minute);
        }
        else
            canvasManager.CloseTimer();


        if (activeLevelScriptable.levelType != LevelType.DefaultLevel)
        {
            ActionManager.CubeCollected += CubeCollected;
            StartCoroutine(GetCubes());
        }

        gameManager.GameWin += GameEnd;
    }

    private void GameEnd()
    {
        gameManager.GameWin -= GameEnd;
        ActionManager.CubeCollected -= CubeCollected;
    }

    private void CubeCollected(int count)
    {
        collectedCubeCount = count;
    }

    private void Update()
    {
        if (!gameManager.ExecuteGame) return;

        if (activeLevelScriptable.levelType != LevelType.DefaultLevel)
            TimeChallenge();
    }

    private void TimeChallenge()
    {
        if (second <= 0)
        {
            if (minute > 0)
            {
                minute--;
                second = 60;
            }
            else
            {
                gameActive = false;
                gameManager.GameWin?.Invoke();
                ActionManager.TimeChallengeEnd?.Invoke(collectedCubeCount);
            }
        }
        second -= Time.deltaTime;

        canvasManager.SetTimer(minute, second);
    }

    IEnumerator GetCubes()
    {
        PoolManager poolManager = PoolManager.Instance;
        int cubesLimit = activeLevelScriptable.levelCubeLimit;
        yield return new WaitForSeconds(.5f);
        
        while (gameActive)
        {
            if (poolManager.ActivesCount < cubesLimit)
            {
                Vector3 ranomPos = new Vector3(Random.Range(-10f, 10f), .5f, Random.Range(-2f, 2f));
                GameObject cube = ActionManager.GetItemFromPool(PoolType.Cube, ranomPos, cubesParent);
            }
            yield return new WaitForSeconds(.2f);
        }
    }
}

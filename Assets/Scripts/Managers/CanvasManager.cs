using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get => instance; set => instance = value; }

    private static CanvasManager instance;


    [SerializeField] private GameObject levelComplatedPanel;
    [SerializeField] private GameObject timerArea;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelComplatedText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private Image clockImage;

    private GameManager gameManager;

    private int clockClearTime = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    void Start()
    {
        gameManager = GameManager.Instance;

        levelText.text = "Lv. " + PrefManager.GetLevel;

        levelComplatedPanel.SetActive(false);

        gameManager.GameStart += GameStart;
        gameManager.GameWin += GameWin;

        ActionManager.TimeChallengeEnd += ChallengeEnd;
    }

    private void ChallengeEnd(int count)
    {
        levelComplatedText.text = count + " Cube Collected!";
    }

    private void GameStart()
    {
        if (timerArea.activeSelf)
            DOTween.To(() => clockImage.fillAmount, x => clockImage.fillAmount = x, 0, clockClearTime * 60)
                .SetEase(Ease.Linear)
                .SetId(GetHashCode());
    }

    private void GameWin()
    {
        int levelIndex = PrefManager.GetLevel;
        levelComplatedText.text = levelIndex + "." + " Level Complated!";
        levelComplatedPanel.SetActive(true);
        levelComplatedPanel.transform.DOScale(Vector3.zero, .5f).From()
            .SetEase(Ease.OutBack)
            .SetId(GetHashCode())
            .OnComplete(() =>
            {
                levelComplatedPanel.transform.DOScale(Vector3.zero, .25f)
                    .SetDelay(1.275f)
                    .SetEase(Ease.OutBack)
                    .SetId(GetHashCode());
            });
    }


    public void OpenTimer(int time)
    {
        timerArea.SetActive(true);
        timerArea.transform.DOScale(Vector3.zero, .75f).From()
            .SetId(GetHashCode())
            .SetEase(Ease.OutBack);

        clockClearTime = time;

        SetTimer(time, 0);
    }

    public void CloseTimer()
    {
        timerArea.SetActive(false);
    }

    public void SetTimer(int minute, float second)
    {
        if (minute < 0 || second < 0) return;

        timerText.text = minute + ":" + Mathf.Floor(second);
    }
}

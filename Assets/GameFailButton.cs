using UnityEngine;

public class GameFailButton : MonoBehaviour
{
    public void RetryButton()
    {
        GameManager.Instance.RestartLevel();
    }
}

using TMPro;
using UnityEngine;

public class MiniGameSpeedAmount : MonoBehaviour
{
    [SerializeField] private TextMeshPro[] poolText;
    private LevelData _data;
    private int _currentTotal;

    private void Awake()
    {
        _data = GetLevelData();
    }
    private LevelData GetLevelData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()];
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onMiniGameBar += OnMiniGameBar;
    }

    private void OnMiniGameBar()
    {
        _currentTotal = 0;

        foreach (TextMeshPro text in poolText)
        {
            int number;
            if (int.TryParse(text.text, out number))
            {
                _currentTotal += number;
            }
        }
        LevelPanelController.Instance.MiniGameBarImage.fillAmount = (float)_currentTotal / _data.total;
        Debug.Log("currentTotal: " + _currentTotal);
        Debug.Log("data.total: " + _data.total);
        Debug.Log("fillImage.fillAmount: " + LevelPanelController.Instance.MiniGameBarImage.fillAmount);
    }

    private void UnSubscribeEvents()
    {
        UISignals.Instance.onMiniGameBar -= OnMiniGameBar;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}

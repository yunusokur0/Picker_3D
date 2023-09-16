using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
    [SerializeField] private TextMeshPro poolText;
    [SerializeField] private TextMeshPro ballsCollectText;
    [SerializeField] private byte stageID;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private float3 poolAfterColor = new float3(0.1607843f, 0.3144797f, 0.6039216f);

    #endregion

    #region Private Variables

     private PoolData _data;
     private byte _collectedCount;

    private readonly string _collectable = "Collectable";

    #endregion

    #endregion

    private void Awake()
    {
        _data = GetPoolData();
    }

    private PoolData GetPoolData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level")
            .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].PoolList[stageID];
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
        CoreGameSignals.Instance.onStageAreaSuccessful += OnChangePoolColor;
    }

    private void OnActivateTweens(byte stageValue)
    {
        if (stageValue != stageID) return;
        foreach (var tween in tweens)
        {
            tween.DOPlay();
        }
    }

    private void OnChangePoolColor(byte stageValue)
    {
        if (stageValue != stageID) return;
        renderer.material.DOColor(new Color(poolAfterColor.x, poolAfterColor.y, poolAfterColor.z, 1), .5f)
            .SetEase(Ease.Flash)
            .SetRelative(false);
    }

    private void Start()
    {
        SetRequiredAmountText();
    }

    private void SetRequiredAmountText()
    {
        poolText.text = $"/{_data.RequiredObjectCount}";
    }

    public bool TakeResults(byte managerStageValue)
    {
        if (stageID == managerStageValue)
        {
            return _collectedCount >= _data.RequiredObjectCount;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_collectable)) return;
        IncreaseCollectedAmount();
        other.gameObject.SetActive(false);
        SetCollectedAmountToPool();
    }

    private void IncreaseCollectedAmount()
    {
        _collectedCount++;
    }

    private void SetCollectedAmountToPool()
    {
        ballsCollectText.text = $"{_collectedCount}";
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateTweens;
        CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangePoolColor;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
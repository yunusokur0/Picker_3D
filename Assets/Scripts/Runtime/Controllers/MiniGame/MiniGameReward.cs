using UnityEngine;

public class MiniGameReward : MonoBehaviour
{
    [SerializeField] private byte stageID;
    private MiniGameRewardData _data;
    private readonly string _player = "Player";
    private readonly string _dataPath = "Data/CD_MiniGameReward";
    
    private void Awake()
    {
        _data = GetPoolData();
    }

    private MiniGameRewardData GetPoolData()
    {
        return Resources.Load<CD_MiniGameReward>(_dataPath).Rewards[stageID];

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_player))
        {
            if (rewardmanager.Instance.rewardText != null)
            {
                rewardmanager.Instance.rewardText.text = $"{_data.Reward}$";
            }
        }
    }
}
